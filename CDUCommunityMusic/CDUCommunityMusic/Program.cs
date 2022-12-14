using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CDUCommunityMusic.Data;
using CDUCommunityMusic.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CDUCommunityMusicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CDUCommunityMusicContext") ?? throw new InvalidOperationException("Connection string 'CDUCommunityMusicContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// add Razor Templating
builder.Services.AddRazorTemplating();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
