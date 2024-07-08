using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class MoviesFanPage
    {
        public static By FavoriteMovie = By.CssSelector(".favoriteMovie");
        public static By BackToMainPage = By.CssSelector(".backToMainPage");
        public static By DeleteLastFavoriteMovie = By.XPath("//a[@class='delete-link']");
    }
}
