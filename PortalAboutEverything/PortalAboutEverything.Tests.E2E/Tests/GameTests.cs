using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using System.Reflection;

namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class GameTests
    {
        private IWebDriver driver;

        public const string GAME_INDEX_URL = "Game/Index";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void GameCreation()
        {
            driver.LoginAsAdmin();

            driver.Url = CommonConstants.BASE_URL + GAME_INDEX_URL;

            var gameBeforeAddOneMore = driver.FindElements(GamePage.GameBlock).Count();

            driver.FindElement(GamePage.CreateLink)
                .Click();

            driver.FindElement(GameCreatePage.NameInput)
                .SendKeys("E2E Game Test");
            driver.FindElement(GameCreatePage.DescriptionInput)
                .SendKeys("E2E Game Desc");
            driver.FindElement(GameCreatePage.YearOfReleaseInput)
                .SendKeys("2001");
            driver.FindElement(GameCreatePage.CoverFileUpload)
                .SendKeys(GetPathToDefaultCover());
            driver.FindElement(GameCreatePage.SubmitButton).Click();

            var gameAfterAddOneMore = driver.FindElements(GamePage.GameBlock).Count();
            Assert.That(gameAfterAddOneMore, Is.EqualTo(gameBeforeAddOneMore + 1));

            driver.FindElement(GamePage.LastGameDeleteLink).Click();

            var gameAfterDeleteLastOne = driver.FindElements(GamePage.GameBlock).Count();

            Assert.That(gameAfterDeleteLastOne, Is.EqualTo(gameBeforeAddOneMore));
        }

        private string GetPathToDefaultCover()
        {
            var pathToCurrentAssembly = Assembly.GetExecutingAssembly().Location;
            var pathToCurrentFolder = Path.GetDirectoryName(pathToCurrentAssembly);
            return Path.Combine(pathToCurrentFolder, "ResourceForTet", "Images", "1.jpg");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
