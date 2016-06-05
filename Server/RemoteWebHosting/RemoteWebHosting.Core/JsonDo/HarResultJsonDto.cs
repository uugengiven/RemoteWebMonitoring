using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RemoteWebHosting.Core.JsonDo
{
    public class HarResultJsonDto
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty("nodeId", NullValueHandling = NullValueHandling.Ignore)]
        public String NodeId { get; set; }
        [JsonProperty("nodeName", NullValueHandling = NullValueHandling.Ignore)]
        public String NodeName { get; set; }
        [JsonProperty("testTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime TestTime { get; set; }
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }
        [JsonProperty("dns", NullValueHandling = NullValueHandling.Ignore)]
        public string Dns { get; set; }
        [JsonProperty("geoFrom", NullValueHandling = NullValueHandling.Ignore)]
        public GeoLocation GeoFrom { get; set; }
        [JsonProperty("geoTo", NullValueHandling = NullValueHandling.Ignore)]
        public GeoLocation GeoTo { get; set; }
        [JsonProperty("testId", NullValueHandling = NullValueHandling.Ignore)]
        public int TestId { get; set; }
        [JsonProperty("ping", NullValueHandling = NullValueHandling.Ignore)]
        public int Ping { get; set; }
        [JsonProperty("totalTime", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalTime { get; set; }
        [JsonProperty("totalSize", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalSize { get; set; }
    }
}
