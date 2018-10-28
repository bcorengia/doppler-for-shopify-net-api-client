﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Doppler.Shopify.ApiClient.Filters
{
    /// <summary>
    /// Options for filtering <see cref="InventoryLevelService.List(InventoryLevelFilter)"/> results.
    /// </summary>
    public class InventoryLevelFilter : ListFilter
    {
        /// <summary>
        /// Ids of inventory items to retrieve
        /// </summary>
        [JsonProperty("inventory_item_ids")]
        public IEnumerable<long> InventoryItemIds { get; set; }

        /// <summary>
        /// Ids of locations to search
        /// </summary>
        [JsonProperty("location_ids")]
        public IEnumerable<long> LocationIds { get; set; }
    }
}