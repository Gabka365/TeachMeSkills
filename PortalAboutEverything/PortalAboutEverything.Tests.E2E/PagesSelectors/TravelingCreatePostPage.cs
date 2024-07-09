using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public static class TravelingCreatePostPage
    {
        public static By InputName = By.CssSelector(".input-name");
        public static By TextAreaReview = By.CssSelector(".text-area-review");
        public static By UploadFile = By.CssSelector(".upload-file"); 
        public static By ButtonCreated = By.CssSelector(".button-created");         
    }
}
