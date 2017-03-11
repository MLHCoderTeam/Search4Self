using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Search4Self.Parser.Models
{
    public class SeenVideosHistoryModel
    {
        public SeenVideosHistoryModel()
        {
            Categories = new List<string>();
            Histogram = new Dictionary<DateTime, IDictionary<string, int>>();
        }

        [JsonProperty("categories")]
        public IList<string> Categories { get; set; }

        [JsonProperty("histogram")]
        public IDictionary<DateTime, IDictionary<string, int>> Histogram { get; set; }
    }
}
