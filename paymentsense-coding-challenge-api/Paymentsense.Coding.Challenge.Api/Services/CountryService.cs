using LazyCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Configs;
using Paymentsense.Coding.Challenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<AppSettings> _settings;
        private readonly IConfiguration _config;
        private readonly IAppCache _appCache;

        public CountryService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> settings, IAppCache appCache)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _appCache = appCache ?? throw new ArgumentNullException(nameof(appCache));
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var hrs = _settings.Value.CacheDurationsHours;
            Func<Task<IEnumerable<Country>>> countryObjectFactory = () => PopulateCountriesCache();
            var data = await _appCache.GetOrAddAsync("Countries", countryObjectFactory, DateTimeOffset.Now.AddHours(hrs));
            return data;
        }

        private async Task<IEnumerable<Country>> PopulateCountriesCache()
        {
            var url = _settings.Value.CountryEndPoint;
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Country>>(result);
        }
    }
}
