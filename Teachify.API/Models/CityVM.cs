using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teachify.API.Models
{
    public class CityVM
    {
        [JsonProperty(PropertyName = "city")]
        public string city { get; set; }
        [JsonProperty(PropertyName = "code")]
        public string code { get; set; }
        [JsonProperty(PropertyName = "admin")]
        public string admin { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string country { get; set; }
        [JsonProperty(PropertyName = "iso2")]
        public string iso2 { get; set; }
        [JsonProperty(PropertyName = "population_proper")]
        public string population_proper { get; set; }
        [JsonProperty(PropertyName = "capital")]
        public string capital { get; set; }
        [JsonProperty(PropertyName = "lat")]
        public string lat { get; set; }
        [JsonProperty(PropertyName = "lng")]
        public string lng { get; set; }
        [JsonProperty(PropertyName = "population")]
        public string population { get; set; }
    }
}
