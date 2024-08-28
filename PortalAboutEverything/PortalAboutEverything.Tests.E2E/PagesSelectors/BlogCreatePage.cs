using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public class BlogCreatePage
    {
        public static By MessageInput = By.CssSelector("#Message");
        public static By ImageInput = By.CssSelector("[type=file]");
        public static By CreatePostButton = By.CssSelector("[type=submit]");
    }
}
