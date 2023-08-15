using BookStore.Data;
using BookStore.Utilities.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<SeederHandler>();

//builder.Services.AddScoped<BookStoreDbContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                   .AddCookie(option1 =>
                                   {
                                       option1.LoginPath = "/Auth/SignIn";
                                       option1.ExpireTimeSpan = TimeSpan.FromSeconds(10);
                                       option1.SlidingExpiration = false;
                                       option1.Events.OnRedirectToLogout = context =>
                                       {
                                           // Lakukan logika tambahan sebelum logout
                                           // Misalnya, catat waktu logout atau hapus sesi lainnya, dan sebagainya.
                                           Console.WriteLine("SINGOUT ===================");

                                           return Task.CompletedTask;
                                       };

                                       option1.Events.OnSigningOut = context =>
                                       {
                                           // Lakukan logika tambahan sebelum logout
                                           // Misalnya, catat waktu logout atau hapus sesi lainnya, dan sebagainya.
                                           Console.WriteLine("SINGOUT ===================");

                                           return Task.CompletedTask;
                                       };

                                       option1.Events.OnCheckSlidingExpiration = context =>
                                       {
                                           Console.WriteLine("SINGOUT Sliding ===================");
                                           Console.WriteLine(context.RemainingTime);
                                           return Task.CompletedTask;
                                       };

                                       option1.Events.OnRedirectToLogin = context =>
                                       {
                                           Console.WriteLine("in on login =====================");
                                           context.Response.Cookies.Append("expired", "true");
                                           context.Response.Redirect(context.RedirectUri);
                                           return Task.CompletedTask;
                                       };
                                   });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// contoh jalankan perintah : dotnet run seeddata --project C:\Users\Febrianto\Desktop\latihan-asp-mvc\BookStore\BookStore\BookStore.csproj
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeederHandler>();
        service.RemoveAllData();
        service.SeedData();
    }
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
    {
        response.Redirect("/Error/Unauthorize");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
    {
        response.Redirect("/Error/NotFound");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
    {
        response.Redirect("/Error/Forbidden");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.MethodNotAllowed))
    {
        response.Redirect("/Error/MethodNotAllowed");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.InternalServerError))
    {
        response.Redirect("/Error/InternalServerError");
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
