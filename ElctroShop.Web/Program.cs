using ElctroShop.Infra.Data.Context;
using ElctroShop.IoC;
using ElctroShop.Web.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.RegisterServices();


#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
    {
        options.LoginPath = "/Login-User";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan= TimeSpan.FromDays(30);
    });
#endregion

#region Persian SweetAlert
builder.Services.AddSingleton<HtmlEncoder>(
    HtmlEncoder.Create(allowedRanges: new[] {UnicodeRanges.BasicLatin,

    UnicodeRanges.Arabic}));
#endregion


#region DbContext
builder.Services.AddDbContext<ElctroShopContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("elctroConnection"));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<CheckAdminMiddleWare>();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
