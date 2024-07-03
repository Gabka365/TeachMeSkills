using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class TravelingPostsPage
    {
        public static By CreatedTravelingPost = By.CssSelector(".btn-create-post");
        public static By PostImage = By.CssSelector(".post-image");
        public static By ButtonDeletePost = By.CssSelector(".del-post");
        public static By InputLastNews = By.CssSelector(".traveling-news-text-input");
        public static By CreateLastNews = By.CssSelector(".traveling-news-btn");
        public static By DeleteLastNews = By.CssSelector(".traveling-news-btn-del");
        public static By News = By.CssSelector(".traveling-news-text");
    }
}
    