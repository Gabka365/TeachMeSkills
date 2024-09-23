using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class HomePage
    {
        public static By UserNameSpan = By.CssSelector(".user-name");
        public static By TravelingLink = By.CssSelector(".traveling_link");
        public static By BlogLink = By.CssSelector(".blog-link");
    }
}
