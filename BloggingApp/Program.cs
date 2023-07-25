using BloggingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BloggingDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingConnectionString")));
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<BloggingDBContext>().AddDefaultTokenProviders();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseIdentity();
app.UseRouting();

app.UseAuthorization();
//app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

SeedAdminUser();
app.Run();



void SeedAdminUser()
{
    //SignInManager<AppUser> loginManager
 using (var scope = app.Services.CreateScope())
    try
    {
        var scopedContext = scope.ServiceProvider.GetRequiredService<BloggingDBContext>();
        var signInManagerContext = scope.ServiceProvider.GetRequiredService<SignInManager<AppUser>>();
            AppUser.SeedAdminUser(scopedContext, signInManagerContext);
    }
    catch
    {
        throw;
    }
}
   