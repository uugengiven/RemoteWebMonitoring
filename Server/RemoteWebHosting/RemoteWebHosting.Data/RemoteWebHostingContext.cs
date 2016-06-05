using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWebHosting.Data
{
    public class RemoteWebHostingContext : DbContext
    {
        public virtual IDbSet<WebTest> WebTests { get; set; }
        public virtual IDbSet<HarData> HarDatas { get; set; }
        public virtual IDbSet<HarResult> HarResults { get; set; }


        public RemoteWebHostingContext(): this("DefaultConnection")
        {
            Database.CommandTimeout = 180;
        }

        public RemoteWebHostingContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public int SaveContext()
        {
            return 0;
            //DateTime now = DateTime.UtcNow;
            //var a = ChangeTracker.Entries();
            //ChangeTracker.Entries().Where(o => o.Entity is Required)
            //    .Select(o => o.Cast<Required>()).ToList()
            //    .ForEach(o => UpdateSeiRequiredFieldsFor(o.Entity, asUser, now, o.State));
        }
    }
}
