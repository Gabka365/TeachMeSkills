using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public class MoviePage
    {
        public static By CreateLink = By.CssSelector(".create-link");
        public static By MovieBlock = By.CssSelector(".movie");

        public static By LastMovieDeleteLink = By.XPath("//div[@class='deleteMovie'][last()]");

        public static By LastMovieCoverLink = By.XPath("//img[@class='movieImage'][last()]");

        public static By MoviesFanLink = By.CssSelector(".moviesFan-link");
        public static By AddToMoviesFan = By.XPath("//input[@class='addToMoviesFan']");
    }
}
