using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class GamePage
    {
        public static By CreateLink = By.CssSelector(".create-link");
        public static By GameBlock = By.CssSelector(".game");

        //public static By LastGameDeleteLink = By.CssSelector(".delete:last");
        public static By LastGameDeleteLink = By.XPath("//a[@class='delete'][last()]");
    }
}
