using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PortalAboutEverything.Tests.E2E.PagesSelectors;

namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class HomePageTests
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void CheckLoginOrLogoutIsVisible()
        {
            driver.Url = CommonConstants.BASE_URL;

            var loginLink = driver.FindElements(Layout.LoginLink);

            var logoutLink = driver.FindElements(Layout.LogoutLink);

            Assert.That(loginLink.Count + logoutLink.Count, Is.EqualTo(1));
        }

        [Test]
        public void AfterLoginAsAdminISeeMyName()
        {
            driver.LoginAsAdmin();

            var userNameOnPage = driver.FindElement(HomePage.UserNameSpan)
                .Text;

            Assert.That(userNameOnPage, Is.EqualTo("admin"));
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
