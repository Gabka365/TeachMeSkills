using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Services;

namespace PortalAboutEverything.Tests.Services
{
    public class PathHelperTest
    {
        public const string FAKE_PROJECT_PATH = "C:\\project";

        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private PathHelper _pathHelper;

        [SetUp]
        public void SetUp()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _pathHelper = new PathHelper(_webHostEnvironmentMock.Object);
        }

        [Test]
        public void GetPathToTravelingImageFolder()
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);
           
            // Act
            var result = _pathHelper.GetPathToTravelingImageFolder();

            // Assert
            Assert.That(result, Is.EqualTo("C:\\project\\images\\Traveling\\UserPictures"));
        }

        [Test]
        [TestCase(5, "C:\\project\\images\\Game\\cover-5.jpg")]
        [TestCase(79, "C:\\project\\images\\Game\\cover-79.jpg")]
        public void GetPathToGameCover(int gameId, string resultPath)
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToGameCover(gameId);

            // Assert
            Assert.That(result, Is.EqualTo(resultPath));
        }

        [Test]
        [TestCase(25, "C:\\project\\images\\BoardGame\\mainImage-25.jpg")]
        [TestCase(995, "C:\\project\\images\\BoardGame\\mainImage-995.jpg")]
        public void GetPathToBoardGameMainImage(int boardGameId, string resultPath)
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToBoardGameMainImage(boardGameId);

            // Assert
            Assert.That(result, Is.EqualTo(resultPath));
        }

        [Test]
        public void GetPathToBoardGameSideImage()
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToBoardGameSideImage(5);

            // Assert
            Assert.That(result, Is.EqualTo("C:\\project\\images\\BoardGame\\sideImage-5.jpg"));
        }

        [Test]
        [TestCase(12, "C:\\project\\images\\Movie\\cover-12.jpg")]
        [TestCase(35, "C:\\project\\images\\Movie\\cover-35.jpg")]
        public void GetPathToMovieImage(int movieId, string path)
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToMovieImage(movieId);

            // Assert
            Assert.That(result, Is.EqualTo(path));
        }


        [Test]
        [TestCase(14, "C:\\project\\images\\Blog\\cover-14.jpg")]
        [TestCase(81, "C:\\project\\images\\Blog\\cover-81.jpg")]
        public void GetPathToPostCover(int postId, string path)
        {
            // Prepare
            _webHostEnvironmentMock
                .Setup(x => x.WebRootPath)
                .Returns(FAKE_PROJECT_PATH);

            // Act
            var result = _pathHelper.GetPathToPostCover(postId);

            // Assert
            Assert.That(result, Is.EqualTo(path));
        }
    }
}
