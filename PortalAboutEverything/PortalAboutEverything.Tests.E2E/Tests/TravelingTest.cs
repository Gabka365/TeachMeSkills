using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using System.Collections.ObjectModel;


namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class TravelingTest
    {
        private const string POST_NAME = "Путешествие в горы";
        private const string POST_REVIEW = "Путешествие выдалось очень крутым";
        private const string NEWS = "Некоторые, конечно, планировали свои путешествия еще зимой, " +
                                            "но в туристической отрасли есть и другая практика";
        private IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();

        }

        [Test]
        public void TravelingPostCreationAndDelete()
        {
            driver.LoginTravelinAdmin();

            driver.FindElement(HomePage.TravelingLink).Click();

            var travelingPostLinkCount = driver.FindElements(TravelingPage.TravelingPostsLink).Count();

            Assert.That(travelingPostLinkCount, Is.EqualTo(1));

            driver.FindElement(TravelingPage.TravelingPostsLink).Click();

            var createdTraveLinkPostCount = driver.FindElements(TravelingPostsPage.CreatedTravelingPost).Count();

            var travelingPostSCount = driver.FindElements(TravelingPostsPage.PostImage).Count();

            Assert.That(createdTraveLinkPostCount, Is.EqualTo(1));

            new Actions(driver).SendKeys(Keys.End).Perform();

            driver.FindElement(TravelingPostsPage.CreatedTravelingPost).Click();

            driver
                .FindElement(TravelingCreatePostPage.InputName)
                .SendKeys(POST_NAME);
            driver
               .FindElement(TravelingCreatePostPage.TextAreaReview)
               .SendKeys(POST_REVIEW);

            driver.FindElement(TravelingCreatePostPage.UploadFile)
               .SendKeys(GetPathToDefaultTravelingCover());

            driver.FindElement(TravelingCreatePostPage.ButtonCreated).Click();

            var travelingPostSCountAfterCreated = driver.FindElements(TravelingPostsPage.PostImage).Count();

            Assert.That(travelingPostSCountAfterCreated, Is.EqualTo(travelingPostSCount + 1));

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(TravelingPostsPage.PostImage);
            IWebElement lastCreatedPostImage = elements.Last();

            lastCreatedPostImage.Click();

            driver.FindElement(TravelingPostsPage.ButtonDeletePost).Click();

            var travelingPostSCountAfterDelete = driver.FindElements(TravelingPostsPage.PostImage).Count();

            Assert.That(travelingPostSCountAfterDelete, Is.EqualTo(travelingPostSCountAfterCreated - 1));

        }

        [Test]
        public void TravelingCreationAndDeleteLastNews()
        {
            driver.LoginAsAdmin();
            driver.FindElement(HomePage.TravelingLink).Click();
            driver.FindElement(TravelingPage.TravelingPostsLink).Click();

            var oldNews = driver.FindElement(TravelingPostsPage.News).Text;

            driver
                .FindElement(TravelingPostsPage.InputLastNews)
                .SendKeys(NEWS);

            driver.FindElement(TravelingPostsPage.CreateLastNews).Click();

            var newNews = driver.FindElement(TravelingPostsPage.News).Text;

            var isNewNews = oldNews != newNews;

            Assert.That(isNewNews, Is.EqualTo(true));

            driver.FindElement(TravelingPostsPage.DeleteLastNews).Click();

            var newsAfterDelete = driver.FindElement(TravelingPostsPage.News).Text;
            var isOldNews = oldNews == newsAfterDelete;
            Assert.That(isOldNews, Is.EqualTo(true));
        }

        public string GetPathToDefaultTravelingCover()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var pathToImageTest = Path.Combine(baseDirectory, "ResourceForTet", "Images", "testTraveling.png");
            return pathToImageTest;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
