using FluentAssertions;
using LazyCache.Mocks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Configs;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServiceTests
    {
        private CountryService _sut;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<IOptions<AppSettings>> _appSettingsMock;

        public CountryServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _appSettingsMock = new Mock<IOptions<AppSettings>>();
            _appSettingsMock.Setup(x => x.Value).Returns(new AppSettings());
            _sut = new CountryService(_httpClientFactoryMock.Object, _appSettingsMock.Object, new MockCachingService());
        }

        [Fact]
        public async Task GetCountries_Should_Return_Countries()
        {
            // arrange
            var countries = new List<Country>
            {
                new Country
                {
                    Name ="test"
                }
            };

            MockHttpClientFactory(countries);

            // act
            var result = await _sut.GetCountries();

            // assert
            result.Should()
                  .BeOfType<List<Country>>()
                  .And
                  .HaveCount(1)
                  .And
                  .Contain(x => x.Name == "test");
        }

        [Fact]
        public void GetCountries_When_No_Dependency_Injection_Throws_Exception()
        {
            // arrange
            // act
            // assert
            Action action = () => new CountryService(null, null, null);

            action.Should().Throw<Exception>();
        }

        private void MockHttpClientFactory(object data)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(data)),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:5001/"),
            };

            _httpClientFactoryMock.Setup(x => x.CreateClient("")).Returns(httpClient).Verifiable();
        }
    }
}
