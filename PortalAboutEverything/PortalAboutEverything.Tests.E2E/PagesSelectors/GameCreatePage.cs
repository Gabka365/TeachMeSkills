using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class GameCreatePage
    {
        public static By NameInput = By.CssSelector("#Name");
        public static By DescriptionInput = By.CssSelector("#Description");
        public static By YearOfReleaseInput = By.CssSelector("#YearOfRelease");
        public static By CoverFileUpload = By.CssSelector("[name=Cover]");
        public static By SubmitButton = By.CssSelector("[type=submit]");
    }
}
