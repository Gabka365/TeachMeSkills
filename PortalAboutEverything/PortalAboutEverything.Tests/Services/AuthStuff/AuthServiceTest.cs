using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Services.AuthStuff;

namespace PortalAboutEverything.Tests.Services.AuthStuff
{
    public class AuthServiceTest
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IUserRepository> _userRepositoryMock;

        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _authService = new AuthService(
                _httpContextAccessorMock.Object,
                _userRepositoryMock.Object);
        }

        [Test]
        public void IsAuthenticated_UserAuthenticated()
         {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Identity.IsAuthenticated)
                .Returns(true);

            // Act
            var result = _authService.IsAuthenticated();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAuthenticated_UserNotAuthenticated()
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext.User.Identity)
                .Returns<System.Security.Principal.IIdentity>(null);

            // Act
            var result = _authService.IsAuthenticated();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsAuthenticated_ThorwExeptionIfHttpContexIsNull()
        {
            // Prepare
            _httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns<HttpContext>(null);

            // Act
            // Assert
            Assert.Throws<NullReferenceException>(() => 
                _authService.IsAuthenticated());
        }
    }
}
