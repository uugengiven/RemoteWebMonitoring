using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWebHosting.Data
{
    public class WebTest:Required
    {
        [Key]
        public long Id { get; set; }
    }
}
