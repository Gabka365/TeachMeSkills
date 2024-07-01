using OpenQA.Selenium;
using PortalAboutEverything.Tests.E2E.PagesSelectors;

namespace PortalAboutEverything.Tests.E2E
{
    public static class DriverLoginExtension
    {
        public const string ADMIN_NAME = "admin";
        public const string ADMIN_PASSWORD = "admin";

        public static void LoginAsAdmin(this IWebDriver driver)
        {
            driver.Url = CommonConstants.BASE_URL;

            driver
                .FindElement(Layout.LoginLink)
                .Click();
            driver
                .FindElement(LoginPage.LoginInput)
                .SendKeys(ADMIN_NAME);
            driver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys(ADMIN_PASSWORD);
            driver
                .FindElement(LoginPage.SubmitButton)
                .Click();
        }
    }
}
