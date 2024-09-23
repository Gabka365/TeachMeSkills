using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace PortalAboutEverything.Tests.E2E.PagesSelectors
{
    public class BlogPage
    {
        public static By CreatePostLink = By.CssSelector(".create-post");
        public static By PostBlock= By.CssSelector(".post");
        public static By LastPostLink = By.XPath("(//div[contains(@class, 'post')])[last()]");
        public static By PostDeleteLink = By.XPath("//a[@class='delete-post'][last()]");
        public static By LastTextBlock = By.XPath("(//div[contains(@class, 'text-of-post')])[last()]");
        public static By LastImageBlock = By.XPath("(//img[contains(@class, 'cover')])[last()]");
    }
}
