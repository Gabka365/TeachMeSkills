using Microsoft.Extensions.DependencyInjection;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.BookClub;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Data
{
    public class Seed
    {
        public void Fill(IServiceProvider serviceProvider)
        {
            using var service = serviceProvider.CreateScope();

            FillUsers(service);
            FillGames(service);
            FillBoardGames(service);
            FillMovies(service);
            FillBooks(service);
        }

        private void FillGames(IServiceScope service)
        {
            var gameRepositories = service.ServiceProvider.GetService<GameRepositories>()!;

            if (!gameRepositories.Any())
            {
                var halfLife = new Game
                {
                    Name = "Half-Life",
                    Description = "Half-Life",
                    YearOfRelease = 2020
                };
                gameRepositories.Create(halfLife);

                var tetris = new Game
                {
                    Name = "tetris",
                    Description = "tetris",
                    YearOfRelease = 2000
                };
                gameRepositories.Create(tetris);
            }
        }

        private void FillBoardGames(IServiceScope service)
        {
            var boardGameRepositories = service.ServiceProvider.GetService<BoardGameRepositories>()!;

            if (!boardGameRepositories.Any())
            {
                var ticketToRide = new BoardGame
                {
                    Title = "Ticket to Ride: Европа",
                    MiniTitle = "Постройте железные дороги по всей Европе!",
                    Description = "Эта увлекательная игра предлагает захватывающее путешествие из дождливого Эдинбурга в солнечный Константинополь. В настольной игре \"Ticket To Ride: Европа\" Вы посетите величественные европейские города, осталось только взять билет на поезд.",
                    Essence = "\"Билет на поезд: Европа\" (Ticket to Ride: Europe) стала второй в серии настольный игр о путешествиях по железной дороге. Здесь вы можете прокладывать маршруты, соединяя города, пускать новые составы и при случае обгонять соперников по количеству заработанных очков. В настольной игре \"Билет на поезд: Европа\" вы перенесетесь в самые красивые города. В новой игре в распоряжении игрока несколько оригинальных механик, добавились игровые элементы, и более разнообразными стали правила. Невероятные ощущения, динамика и новые открытия ждут вас в этой настольной игре.",
                    Tags = "Игра из серии",
                    Price = 3900,
                    ProductCode = 31458,
                    Reviews = new()
                };
                boardGameRepositories.Create(ticketToRide);
            }
        }

        private void FillBooks(IServiceScope service)
        {
            var bookRepositor = service.ServiceProvider.GetService<BookRepositories>()!;
            if (!bookRepositor.Any())
            {
                var CSharpInDepth = new Book
                {
                    BookAuthor = "Jon Skeet",
                    BookTitle = "C# in Depth",
                    SummaryOfBook = "C# in Depth, Fourth Edition is your key to unlocking the powerful new features added to the language in C# 5," +
                    " 6, and 7. Following the expert guidance of C# legend Jon Skeet, you'll master asynchronous functions, expression-bodied members," +
                    " interpolated strings, tuples, and much more",
                    YearOfPublication = 2019
                };
                bookRepositor.Create(CSharpInDepth);  

                var ASPNETCoreinAction = new Book
                {
                    BookAuthor = "ANDREW LOCK",
                    BookTitle = "ASP.NET Core in Action",
                    SummaryOfBook = "Fantastic book. The topics are explained clearly and thoroughly. It’s well written and researched. " +
                    "If you want a thorough understanding of ASP.NET Core,this is where you need to start.",
                    YearOfPublication = 2019
                };
                bookRepositor.Create(ASPNETCoreinAction);
            }
        }

        private void FillUsers(IServiceScope service)
        {
            var userRepository = service.ServiceProvider.GetService<UserRepository>()!;

            if (!userRepository.Any())
            {
                var admin = new User
                {
                    UserName = "admin",
                    Password = "admin",
                    Role = UserRole.Admin,
                    Language = Language.En
                };
                userRepository.Create(admin);

                var user = new User
                {
                    UserName = "user",
                    Password = "user",
                    Role = UserRole.User,
                    Language = Language.Ru
                };
                userRepository.Create(user);
              
                var travelingAdmin = new User
                {
                    UserName = "travelingAdmin",
                    Password = "travelingAdmin",
                    Role = UserRole.TravelingAdmin,
                    Language= Language.En   
                };
                userRepository.Create(travelingAdmin);
              
                var videoLibraryAdmin = new User
                {
                    UserName = "ancient",
                    Password = "ancient",
                    Role = UserRole.VideoLibraryAdmin,
                    Language = Language.En
                };
                userRepository.Create(videoLibraryAdmin);

                var gameStoreAdmin = new User
                {
                    UserName = "GameStoreAdmin",
                    Password = "admin",
                    Role = UserRole.GameStoreAdmin,
                    Language = Language.En
                };
                userRepository.Create(gameStoreAdmin);
            }
        }

        public void FillMovies(IServiceScope service)
        {
            var movieRepositories = service.ServiceProvider.GetService<MovieRepositories>()!;
            
            if (!movieRepositories.Any())
            {
                var inception = new Movie
                {
                    Name = "Inception",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                    ReleaseYear = 2010,
                    Director = "Christopher Nolan",
                    Budget = 160_000_000,
                    CountryOfOrigin = "USA",
                };
                movieRepositories.Create(inception);
                
                var insideOut = new Movie
                {
                    Name = "InsideOut",
                    Description = "After young Riley is uprooted from her Midwest life and moved to San Francisco, her emotions - Joy, Fear, Anger, Disgust and Sadness - conflict on how best to navigate a new city, house, and school.",
                    ReleaseYear = 2015,
                    Director = "Pete Docter",
                    Budget = 175_000_000,
                    CountryOfOrigin = "USA",
                };
                movieRepositories.Create(insideOut);
            }
        }
    }
}
