using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Services.VideoLibrary;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PortalDbContext>(x => x.UseSqlServer(PortalDbContext.CONNECTION_STRING));

//VideoLibrary Services
builder.Services.AddScoped<VideoLibraryRepository>();
builder.Services.AddScoped<VideoProcessorService>();
builder.Services.AddSingleton<FfMpegService>();

builder.Services.AddSingleton<TravelingRepositories>();
builder.Services.AddScoped<GameRepositories>();
builder.Services.AddSingleton<BlogRepositories>();

builder.Services.AddScoped<MovieRepositories>();

builder.Services.AddScoped<BoardGameReviewRepositories>();
builder.Services.AddScoped<HistoryRepositories>();
builder.Services.AddSingleton<BookRepositories>();
builder.Services.AddScoped<StoreRepositories>();
builder.Services.AddScoped<GameStoreRepositories>();

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
