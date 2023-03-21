using InforceTestTask.Data.Contexts;
using InforceTestTask.Data.Repositories;
using InforceTestTask.Data.Repositories.Interfaces;
using InforceTestTask.Infrastructure.Filters;
using InforceTestTask.Infrastructure.Services;
using InforceTestTask.Infrastructure.Services.Interfaces;
using InforceTestTask.Services;
using InforceTestTask.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Accounts/Login";
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IAccountsService, AccountsService>();
builder.Services.AddTransient<IUrlsRepository, UrlsRepository>();
builder.Services.AddTransient<IUrlsService, UrlsService>();

builder.Services.AddDbContextFactory<UrlsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UrlsConnection")));
builder.Services.AddScoped<IDbContextWrapper<UrlsDbContext>, DbContextWrapper<UrlsDbContext>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
