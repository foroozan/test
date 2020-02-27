using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryHttpService;

        public CountryController(ICountryService countryHttpService)
        {
            _countryHttpService = countryHttpService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Country>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryHttpService.GetCountries();

            if (countries == null || !countries.Any())
            {
                return NotFound();
            }

            return Ok(countries);
        }
    }
}
