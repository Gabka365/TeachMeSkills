using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PortalAboutEverything.Tests.E2E.PagesSelectors;
using System.Security.Cryptography;
using System.IO;

namespace PortalAboutEverything.Tests.E2E.Tests
{
    public class BlogTests
    {
        private IWebDriver _webDriver;
        private IJavaScriptExecutor _js;
        private string _testText = "This is the text for End-to-End test.";

        public const string BLOG_INDEX_URL = "Blog/Index";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _webDriver = new ChromeDriver();
            _js = (IJavaScriptExecutor)_webDriver;
        }

        [Test]
        public void PostCreation()
        {
            _webDriver.LoginAsAdmin();
            _webDriver.FindElement(HomePage.BlogLink)
                .Click();
            var postBeforeAddOneMore = _webDriver.FindElements(BlogPage.PostBlock).Count();
            Thread.Sleep(1000);


            _webDriver.FindElement(BlogPage.CreatePostLink)
                .Click();
            _webDriver.FindElement(BlogCreatePage.MessageInput)
                .SendKeys(_testText);
            _webDriver.FindElement(BlogCreatePage.CreatePostButton)
                .Click();
            var postAfterAddOneMore = _webDriver.FindElements(BlogPage.PostBlock).Count();
            Assert.That(postAfterAddOneMore, Is.EqualTo(postBeforeAddOneMore + 1));


            IWebElement lastPostLink = _webDriver.FindElement(BlogPage.LastPostLink);
            _js.ExecuteScript("arguments[0].click();", lastPostLink);
            IWebElement postDeleteLink = _webDriver.FindElement(BlogPage.PostDeleteLink);
            _js.ExecuteScript("arguments[0].click();", postDeleteLink);
            postAfterAddOneMore = _webDriver.FindElements(BlogPage.PostBlock).Count();
            Assert.That(postAfterAddOneMore, Is.EqualTo(postBeforeAddOneMore));
        }

        [Test]
        public async Task DataFillingWithPostCreation()
        {
            _webDriver.LoginAsAdmin();
            _webDriver.FindElement(HomePage.BlogLink)
                .Click();
            var postBeforeAddOneMore = _webDriver.FindElements(BlogPage.PostBlock).Count();
            Thread.Sleep(1000);


            _webDriver.FindElement(BlogPage.CreatePostLink)
                .Click();
            _webDriver.FindElement(BlogCreatePage.MessageInput)
                .SendKeys(_testText);
            _webDriver.FindElement(BlogCreatePage.ImageInput)
                .SendKeys(GetPathToDefaultCover());
            _webDriver.FindElement(BlogCreatePage.CreatePostButton)
                .Click();
            
            IWebElement lastPostLink = _webDriver.FindElement(BlogPage.LastPostLink);
            _js.ExecuteScript("arguments[0].click();", lastPostLink);
            Thread.Sleep(1000);


            IWebElement lastTextBox = _webDriver.FindElement(BlogPage.LastTextBlock);
            Assert.That(lastTextBox.Text, Is.EqualTo(_testText));

            IWebElement lastImageBox = _webDriver.FindElement(BlogPage.LastImageBlock);
            string actualImageUrl = lastImageBox.GetAttribute("src");


            bool areImagesIdentical = await CompareImagesByUrlAsync(actualImageUrl, GetPathToDefaultCover());
            Assert.That(areImagesIdentical, Is.True);


            IWebElement postDeleteLink = _webDriver.FindElement(BlogPage.PostDeleteLink);
            _js.ExecuteScript("arguments[0].click();", postDeleteLink);
            var checkSumPost = _webDriver.FindElements(BlogPage.PostBlock).Count();
            Assert.That(checkSumPost, Is.EqualTo(postBeforeAddOneMore));
        }

        private string GetPathToDefaultCover()
        {
            var pathToCurrentAssembly = Assembly.GetExecutingAssembly().Location;
            var pathToCurrentFolder = Path.GetDirectoryName(pathToCurrentAssembly);
            return Path.Combine(pathToCurrentFolder, "ResourceForTet", "Images", "PostImage-default.jpg");
        }

        public async Task<bool> CompareImagesByUrlAsync(string url1, string url2)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] image1Bytes = await client.GetByteArrayAsync(url1);
                byte[] image2Bytes = await File.ReadAllBytesAsync(url2);

                string hash1 = GetImageHash(image1Bytes);
                string hash2 = GetImageHash(image2Bytes);

                return hash1 == hash2;
            }
        }

        public string GetImageHash(byte[] imageBytes)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(imageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webDriver.Quit();
        }
    }
}
