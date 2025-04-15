using WebApp.Configuration;
using WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.InjectDataLayer();
builder.Services.InjectServices();
builder.Services.InjectUtilities();
builder.Services.ConfigureAuthentication(builder.Configuration);

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<JwtCookieMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
