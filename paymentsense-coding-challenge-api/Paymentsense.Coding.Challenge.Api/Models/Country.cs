using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public IList<string> Timezones { get; set; }
    }
}