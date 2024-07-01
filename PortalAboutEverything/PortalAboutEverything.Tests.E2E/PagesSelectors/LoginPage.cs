using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class LoginPage
    {
        public static By LoginInput = By.CssSelector("#Login");
        public static By PasswordInput = By.CssSelector("#Password");
        public static By SubmitButton = By.CssSelector("[type=submit]");
    }
}
