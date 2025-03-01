using Microsoft.EntityFrameworkCore;
using Kick_X.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MyDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// builder.Services.AddSession(options => {
//     options.IdleTimeout = TimeSpan.FromMinutes(10);
// });

// Adding Authentication using Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Customer/Login";
    options.LogoutPath = "/Customer/Logout";
    options.AccessDeniedPath = "/Home/Error404";
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
});

// Adding Authorization to the middleware
builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminOnly", policy =>
    policy.RequireRole("Admin"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Admin/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Home}/{id?}");

app.Run();
