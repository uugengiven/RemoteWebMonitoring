using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWebHosting.Data
{
    public class HarResult:Required
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid NodeId { get; set; }
        public string NodeName { get; set; }
        public DateTimeOffset TestTime { get; set; }
        public string Ip { get; set; }
        public string Dns { get; set; }
        public Geo From { get; set; }
        public Geo To { get; set; }
        public int Ping { get; set; }
        public long TotalTime { get; set; }
        public long TotalSize { get; set; }

        public virtual HarData HarData { get; set; }
        public virtual WebTest Test { get; set; }
    }

    public class Geo
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
