using OpenQA.Selenium;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public class BoardGamePage
    {
        public static By CreateReviewButton = By.CssSelector(".create-review-button");
        public static By ReviewsContainer = By.CssSelector(".reviews-container");
        public static By Review = By.CssSelector(".review");
    }
}
