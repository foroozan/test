using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryService> _countryHttpServiceMock;
        private readonly CountryController _sut;

        public CountryControllerTests()
        {
            _countryHttpServiceMock = new Mock<ICountryService>();
            _sut = new CountryController(_countryHttpServiceMock.Object);
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_No_Countries()
        {
            // arrange
            _countryHttpServiceMock
                .Setup(x => x.GetCountries())
                .ReturnsAsync((IEnumerable<Country>)null);

            // act
            var result = await _sut.Get();

            // assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Should_Return_Countries_And_Ok_Status()
        {
            // arrange
            var countries = new List<Country>
            {
                new Country
                {
                    Name ="test"
                }
            };

            _countryHttpServiceMock.Setup(x => x.GetCountries()).ReturnsAsync((IEnumerable<Country>)countries);

            // act
            var result = await _sut.Get();

            // assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(countries);
        }
    }
}
