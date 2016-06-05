using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWebHosting.Core.DTO
{
    public class HarResultDto
    {
        public Guid Id { get; set; }
        public Guid NodeId { get; set; }
        public string NodeName { get; set; }
        public DateTimeOffset TestTime { get; set; }
        public string Ip { get; set; }
        public string Dns { get; set; }
        public GeoDto From { get; set; }
        public GeoDto To { get; set; }
        public int Ping { get; set; }
        public long TotalTime { get; set; }
        public long TotalSize { get; set; }
        public HarDataDto HarData { get; set; }
        public WebTestDto WebTest { get; set; }
    }
}
