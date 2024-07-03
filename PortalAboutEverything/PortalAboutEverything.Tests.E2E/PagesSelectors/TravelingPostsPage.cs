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
    }
}
    