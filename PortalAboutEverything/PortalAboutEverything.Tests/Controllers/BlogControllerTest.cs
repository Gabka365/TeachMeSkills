using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PortalAboutEverything.Controllers;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Repositories.Interfaces;
using PortalAboutEverything.Models.Blog;
using PortalAboutEverything.Services;
using PortalAboutEverything.Services.AuthStuff.Interfaces;
using PortalAboutEverything.Services.Interfaces;
using System.Security.Claims;

namespace PortalAboutEverything.Tests.Controllers
{
    public class BlogControllerTest
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<IPathHelper> _pathHelperMock;
        private Mock<IBlogRepositories> _blogRepositoriesMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private BlogController _blogController;


        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _pathHelperMock = new Mock<IPathHelper>();
            _blogRepositoriesMock = new Mock<IBlogRepositories>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _blogController = new BlogController(_blogRepositoriesMock.Object, _authServiceMock.Object, _pathHelperMock.Object);
        }

        [Test]
        [TestCase("user")]
        public void BuildMessageViewModel(string userName)
        {
            //Prepare
            MessageViewModel viewModel = new MessageViewModel();
            viewModel.Name = userName;
            
            _authServiceMock
                .Setup(x => x.GetUserName())
                .Returns(viewModel.Name); 

            
            //Act
            var result = _blogController.BuildMessageViewModel();


            //Assert
            Assert.That(result.Name, Is.EqualTo(viewModel.Name));
        }
    }
}
