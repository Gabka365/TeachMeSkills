using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class MovieCreatePage
    {
        public static By NameInput = By.CssSelector("#Name");
        public static By DescriptionInput = By.CssSelector("#Description");
        public static By ReleaseYearInput = By.CssSelector("#ReleaseYear");
        public static By DirectorInput = By.CssSelector("#Director");
        public static By BudgetInput = By.CssSelector("#Budget");
        public static By CountryOfOriginInput = By.CssSelector("#CountryOfOrigin");
        public static By CoverFileUpload = By.CssSelector("[name=MovieImage]");
        public static By SubmitButton = By.CssSelector("[type=submit]");
    }
}
