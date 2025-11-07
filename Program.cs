using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VLAN_Switching.Data;
using VLAN_Switching.Models;
using VLAN_Switching.Services;
//using VLAN_Switching.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // Require login for all pages
    options.Conventions.AllowAnonymousToPage("/Login"); // Allow anonymous access to Login
});

//builder.Services.AddRazorPages();

// Register AppDbContext with SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AU_ConnString")));

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Redirect here if not authenticated
        options.AccessDeniedPath = "/AccessDenied"; // Optional: handle forbidden
    });


builder.Services.AddDistributedMemoryCache(); // Required
builder.Services.AddSession(); // Required
builder.Services.AddSignalR();
builder.Services.Configure<SshConfigModel>(builder.Configuration.GetSection("SshConfig"));
builder.Services.AddSingleton<SshService>();              // SSH client logic
builder.Services.AddScoped<VlanSwitchingService>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/Logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Session.Clear(); // Optional: clears session if you use it
    return Results.Redirect("/Login"); // Redirect to Login page
});

app.MapRazorPages();
app.Run();
