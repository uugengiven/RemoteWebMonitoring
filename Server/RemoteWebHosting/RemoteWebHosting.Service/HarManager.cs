using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteWebHosting.Core.DTO;
using RemoteWebHosting.Core.JsonDo;
using RemoteWebHosting.Data;

namespace RemoteWebHosting.Service
{
    public class HarManager
    {
        public bool SaveHarData(HarDataDto harData)
        {
            using (var context = new RemoteWebHostingContext())
            {
                HarData har = new HarData
                {
                    Blog = harData.Blog
                };
                context.HarDatas.Add(har);
                return context.SaveChanges()>0;
            }
        }

        public Guid SaveHarResult(HarResultJsonDto harResult)
        {
            using (var context = new RemoteWebHostingContext())
            {
                HarData harData = new HarData();
                //TODO Zach: Fix the default Date Times
                HarResult harRes = new HarResult
                {
                    Dns = harResult.Dns,
                    Ip = harResult.Ip,
                    NodeId = Guid.Parse(harResult.NodeId),
                    NodeName = harResult.NodeName,
                    Ping = harResult.Ping,
                    TestTime = DateTimeOffset.UtcNow,//harResult.TestTime,
                    TotalSize = harResult.TotalSize,
                    TotalTime = harResult.TotalTime,
                    HarData = harData,
                    CreatedOn = DateTimeOffset.UtcNow
                };
                if (harResult.GeoTo != null)
                {
                    harRes.To = new Geo
                    {
                        Latitude = harResult.GeoTo.Latitude,
                        Longitude = harResult.GeoTo.Longitude
                    };
                }
                if (harResult.GeoFrom != null)
                {
                    harRes.From = new Geo
                    {
                        Latitude = harResult.GeoFrom.Latitude,
                        Longitude = harResult.GeoFrom.Longitude
                    };
                }

                context.HarResults.Add(harRes);
                context.HarDatas.Add(harData);
                context.SaveChanges();
                return harData.Id;
            }
        }
    }
}
