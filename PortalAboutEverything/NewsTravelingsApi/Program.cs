using Microsoft.EntityFrameworkCore;
using NewsTravelingsApi.Data;
using NewsTravelingsApi.Data.Model;
using NewsTravelingsApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NewsTravelingsApiDbContext>(x => x.UseSqlServer(NewsTravelingsApiDbContext.CONNECTION_STRING));

builder.Services.AddScoped<NewsRepository>();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        //p.AllowAnyOrigin();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();

var seed = new Seed();
seed.Fill(app.Services);

app.MapGet("/", () => "Hello World!");

app.MapGet("/DtoLastNews", (NewsRepository repository) => new
{
    Text = repository.LastNews().Text,
    Id = repository.LastNews().Id

}); ; ;
app.MapPost("/news/add", async (HttpContext context, NewsRepository repository) =>
{
   
    var userId = context.Request.Form["userId"];
    var inputText = context.Request.Form["text"];

    var newNews = new News
    {
        Text = inputText,
        UserId = int.Parse(userId)
    };
    repository.Create(newNews);
});

app.MapDelete("/news/del", async (HttpContext context, NewsRepository repository) =>
{
    var newsIdString = context.Request.Form["newsId"];
    var text = context.Request.Form["text"]; 

    var news = repository.GetNewsFromTextAndNewsId(text, int.Parse(newsIdString));
    repository.Delete(news);

});

app.Run();
