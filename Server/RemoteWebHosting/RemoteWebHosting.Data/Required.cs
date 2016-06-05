using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWebHosting.Data
{
    public class Required:IRequired
    {
        public DateTimeOffset CreatedOn { get; set; }
    }
}
