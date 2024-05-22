using Microsoft.Extensions.DependencyInjection;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories;

namespace PortalAboutEverything.Data
{
    public class Seed
    {
        public void Fill(IServiceProvider serviceProvider)
        {
            using var service = serviceProvider.CreateScope();

            fillUsers(service);
            fillGames(service);
        }

        private void fillGames(IServiceScope service)
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

        private void fillUsers(IServiceScope service)
        {
            var userRepository = service.ServiceProvider.GetService<UserRepository>()!;

            if (!userRepository.Any())
            {
                var admin = new User
                {
                    UserName = "admin",
                    Password = "admin"
                };
                userRepository.Create(admin);

                var user = new User
                {
                    UserName = "user",
                    Password = "user"
                };
                userRepository.Create(user);
            }
        }
    }
}
