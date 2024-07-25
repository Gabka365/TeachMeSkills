using Moq.Protected;
using Moq;
using NUnit.Framework;
using PortalAboutEverything.Services.Dtos;
using PortalAboutEverything.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PortalAboutEverything.Tests.Services
{
    public class HttpNewsTravelingsApiTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private HttpNewsTravelingsApi _httpNewsTravelingsApi;

        [SetUp]
        public void SetUp()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:7032")
            };
            _httpNewsTravelingsApi = new HttpNewsTravelingsApi(_httpClient);
        }

        [Test]
        [TestCase(1, "Test News 1")]
        [TestCase(2, "Test News 2")]
        [TestCase(3, "Test News 3")]
        public async void GetLastNews(int expectedId, string expectedText)
        {
            // Arrange
            var expectedNews = new DtoLastNews { Id = expectedId, Text = expectedText };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedNews))
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _httpNewsTravelingsApi.GetLastNewsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(expectedId));
            Assert.That(result.Text, Is.EqualTo(expectedText));
        }

        [Test]
        [TestCase(1, "Test News 1")]
        [TestCase(2, "Test News 2")]
        [TestCase(3, "Test News 3")]
        public void GeLastNewsId(int expectedId, string expectedText)
        {
            // Arrange
            var expectedNews = new DtoLastNews { Id = expectedId, Text = expectedText };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedNews))
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = _httpNewsTravelingsApi.GeLastNewsId();

            // Assert
            Assert.That(result, Is.EqualTo(expectedId));
        }
    }
}
