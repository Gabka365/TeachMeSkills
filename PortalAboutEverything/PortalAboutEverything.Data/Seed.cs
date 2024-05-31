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

                var azul = new BoardGame
                {
                    Title = "Азул",
                    MiniTitle = "Украсьте стены королевского дворца!",
                    Description = "\"Азул\" покоряет своим красочным оформлением, глубоким игровым процессом, а также простыми в освоении правилами. От двух до четырёх игроков примут участие в создании неповторимых орнаментов для украшения стен королевского дворца. Игроки по очереди набирают цветную плитку, всякий раз беря её по одному цвету из предлагаемых комплектов, чтобы раньше соперников выполнить заказ по облицовке дворца... Создавая из ярких плиток красивые узоры, игроки получают победные очки, которые и определяют победителя в конце игры.",
                    Essence = "Цель — набрать как можно больше очков, заполняя свой планшет плитками определенным образом и создавая узоры на стене.",
                    Tags = "Игра из серии",
                    Price = 4000,
                    ProductCode = 68216,
                    Reviews = new()
                };
                boardGameRepositories.Create(azul);
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
                };
                userRepository.Create(admin);

                var user = new User
                {
                    UserName = "user",
                    Password = "user",
                    Role = UserRole.User,
                };
                userRepository.Create(user);
            }
        }
    }
}
