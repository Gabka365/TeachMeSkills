using Moq;
using NUnit.Framework;
using PortalAboutEverything.Data.Repositories.DataModel;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Mappers;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Tests.Mappers
{
    internal class BoardGameMapperTest
    {
        private Mock<IAuthService> _authServise;
        private Mock<IPathHelper> _pathHelper;
        private Mock<IBoardGameRepositories> _gameRepositories;
        private BoardGameMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _authServise = new Mock<IAuthService>();
            _pathHelper = new Mock<IPathHelper>();
            _gameRepositories = new Mock<IBoardGameRepositories>();
            _mapper = new BoardGameMapper(_authServise.Object, _pathHelper.Object, _gameRepositories.Object);
        }

        [Test]
        public void BuildFavoriteBoardGameIndexViewModel()
        {
            // Prepare
            var model = new Top3BoardGameDataModel() 
            { 
                Id = 10,
                Title = "Test board game",
                CountOfUserWhoLikeIt = 1
            };

            // Act
            var result = _mapper.BuildFavoriteBoardGameIndexViewModel(model);

            // Assert
            Assert.That(result.Id, Is.EqualTo(10));
            Assert.That(result.Title, Is.EqualTo("Test board game"));
            Assert.That(result.CountOfUserWhoLikeIt, Is.EqualTo(1));
        }
    }
}
