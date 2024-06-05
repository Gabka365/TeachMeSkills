using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Controllers;
using PortalAboutEverything.CustomMiddlewareServices;
using PortalAboutEverything.Data;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;
using PortalAboutEverything.VideoServices.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(AuthController.AUTH_METHOD)
    .AddCookie(AuthController.AUTH_METHOD, option =>
    {
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddDbContext<PortalDbContext>(x => x.UseSqlServer(PortalDbContext.CONNECTION_STRING));

//Repository
builder.Services.AddVideoLibraryServices();
builder.Services.AddScoped<TravelingRepositories>();
builder.Services.AddScoped<GameRepositories>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddSingleton<BlogRepositories>();
builder.Services.AddScoped<MovieRepositories>();
builder.Services.AddScoped<MovieReviewRepositories>();
builder.Services.AddScoped<BoardGameRepositories>();
builder.Services.AddScoped<BoardGameReviewRepositories>();
builder.Services.AddScoped<HistoryRepositories>();
builder.Services.AddScoped<BookRepositories>();
builder.Services.AddScoped<BookReviewRepositories>();
builder.Services.AddScoped<StoreRepositories>();
builder.Services.AddScoped<GoodReviewRepositories>();
builder.Services.AddScoped<GameStoreRepositories>();
builder.Services.AddScoped<CommentRepository>();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<PathHelper>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var seed = new Seed();
seed.Fill(app.Services);

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

app.UseAuthentication(); // Who I am?
app.UseAuthorization(); // May I?

app.UseMiddleware<LocalizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
