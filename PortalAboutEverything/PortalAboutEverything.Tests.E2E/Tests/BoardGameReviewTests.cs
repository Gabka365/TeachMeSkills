using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using OpenQA.Selenium.Support.UI;

namespace PortalAboutEverything.Tests.E2E.Tests
{
    internal class BoardGameReviewTests
    {
        private IWebDriver _driver;
        //private WebDriverWait _wait;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver();
            //_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void BoardGameReviewCRUD()
        {
            _driver.LoginAsAdmin();

            _driver.Url = CommonConstants.BASE_URL;

            _driver.FindElement(Layout.BoardGameLink).Click();
            _driver.FindElement(BoardGameIndexPage.FirstBoardGame).Click();

            //_wait.Until(driver => _driver.FindElement(BoardGamePage.ReviewsContainer).FindElements(By.XPath("./*")).Count > 0);
            Thread.Sleep(5000);

            var reviewsBeforeAddOneMore = _driver.FindElements(BoardGamePage.Review).Count();

            _driver.FindElement(BoardGamePage.CreateReviewButton).Click();

            _driver.FindElement(ReviewPage.TextInput).SendKeys("Test review!");
            _driver.FindElement(ReviewPage.SendButton).Click();

            Thread.Sleep(5000);

            var reviewsAfterAddOneMore = _driver.FindElements(BoardGamePage.Review).Count();

            Assert.That(reviewsBeforeAddOneMore + 1, Is.EqualTo(reviewsAfterAddOneMore));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }
    }
}
