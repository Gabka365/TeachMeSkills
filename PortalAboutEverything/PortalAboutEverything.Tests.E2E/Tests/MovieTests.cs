using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using System.Reflection;

namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class MovieTests
    {
        private IWebDriver driver;

        public const string MOVIE_INDEX_URL = "Movie/Index";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void MovieAddToFanMovies()
        {
            driver.LoginAsAdmin();

            driver.Url = CommonConstants.BASE_URL + MOVIE_INDEX_URL;
            
            driver.FindElement(MoviePage.MoviesFanLink)
                .Click();
            var amountOfMoviesBeforeAdd = driver.FindElements(MoviesFanPage.FavoriteMovie).Count();

            driver.FindElement(MoviesFanPage.BackToMainPage)
                .Click();
            Thread.Sleep(5000);

            driver.FindElement(MoviePage.AddToMoviesFan)
                .Click();

            var amountOfMoviesAfterAdd = driver.FindElements(MoviesFanPage.FavoriteMovie).Count();

            Assert.That(amountOfMoviesAfterAdd, Is.EqualTo(amountOfMoviesBeforeAdd + 1));

            driver.FindElement(MoviesFanPage.DeleteLastFavoriteMovie)
                .Click();

            var amountOfMoviesAfterDelete = driver.FindElements(MoviesFanPage.FavoriteMovie).Count();

            Assert.That(amountOfMoviesAfterDelete, Is.EqualTo(amountOfMoviesBeforeAdd));
        }


        [Test]
        public void MovieCreation()
        {
            driver.LoginAsAdmin();

            driver.Url = CommonConstants.BASE_URL + MOVIE_INDEX_URL;

            var movieBeforeAddOneMore = driver.FindElements(MoviePage.MovieBlock).Count();

            driver.FindElement(MoviePage.CreateLink)
                .Click();

            driver.FindElement(MovieCreatePage.NameInput)
                .SendKeys("Lion King Test");
            driver.FindElement(MovieCreatePage.DescriptionInput)
                .SendKeys("Description Test");
            driver.FindElement(MovieCreatePage.ReleaseYearInput)
                .SendKeys("1994");
            driver.FindElement(MovieCreatePage.DirectorInput)
                .SendKeys("Roger Allers");
            driver.FindElement(MovieCreatePage.BudgetInput)
                .SendKeys("45000000");
            driver.FindElement(MovieCreatePage.CountryOfOriginInput)
                .SendKeys("USA");
            driver.FindElement(MovieCreatePage.CoverFileUpload)
                .SendKeys(GetPathToDefaultCover());
            driver.FindElement(MovieCreatePage.SubmitButton).Click();

            var movieAfterAddOneMore = driver.FindElements(MoviePage.MovieBlock).Count();
            Assert.That(movieAfterAddOneMore, Is.EqualTo(movieBeforeAddOneMore + 1));

            driver.FindElement(MoviePage.LastMovieCoverLink).Click();
            Thread.Sleep(5000);

            driver.FindElement(MoviePage.LastMovieDeleteLink).Click();
            Thread.Sleep(5000);

            var movieAfterDeleteLastOne = driver.FindElements(MoviePage.MovieBlock).Count();
            
            Assert.That(movieAfterDeleteLastOne, Is.EqualTo(movieBeforeAddOneMore));
        }

        private string GetPathToDefaultCover()
        {
            var pathToCurrentAssembly = Assembly.GetExecutingAssembly().Location;
            var pathToCurrentFolder = Path.GetDirectoryName(pathToCurrentAssembly);
            return Path.Combine(pathToCurrentFolder, "ResourceForTet", "Images", "Movie.PNG");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
