using Newtonsoft.Json;

namespace Doppler.Shopify.ApiClient
{
    public class TaxLine
    {
        /// <summary>
        /// The amount of tax to be charged.    
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// The rate of tax to be applied.
        /// </summary>
        [JsonProperty("rate")]
        public decimal? Rate { get; set; }

        /// <summary>
        /// The name of the tax.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
