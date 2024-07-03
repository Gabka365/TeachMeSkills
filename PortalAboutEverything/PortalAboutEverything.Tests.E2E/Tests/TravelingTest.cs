using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using System.Collections.ObjectModel;


namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class TravelingTest
    {
        private const string POST_NAME = "Путешествие в горы";
        private const string POST_REVIEW = "Путешествие выдалось очень крутым";
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

            var travelingPostLinkCount = driver.FindElements(TravelingPage.TravelingPostLink).Count();

            Assert.That(travelingPostLinkCount, Is.EqualTo(1));

            driver.FindElement(TravelingPage.TravelingPostLink).Click();

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
