using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.GuestDTO;
using HanaHotel.WebUI.Models;
using HanaHotel.WebUI.ValidationRules.AdminGuestValidationRules;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using HanaHotel.BusinessLayer.Abstract;
using HanaHotel.BusinessLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ appsettings.json
var urlApi = builder.Configuration["AppSettings:urlAPI"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<DataContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
})
.AddRazorRuntimeCompilation();

// HttpClient factory
builder.Services.AddHttpClient();

// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/Login/Index";
});

// Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Make HttpContext accessible
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IValidator<CreateGuestDTO>, CreateGuestValidator>();

// Remove/AddIdentity registration; instead register authentication cookie:
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Register your user service
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

await DataInitializer.TestDataAsync(app);

app.UseStatusCodePagesWithRedirects("/Error/Error404/?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
