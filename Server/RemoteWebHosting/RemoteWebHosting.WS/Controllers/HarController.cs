
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using RemoteWebHosting.Core.DTO;
using RemoteWebHosting.Core.JsonDo;
using RemoteWebHosting.Service;

namespace RemoteWebHosting.WS.Controllers
{
    public class HarController : ApiController
    {
        [HttpGet]
        public bool Ping()
        {
            return true;
        }
        [HttpPost]
        public string SaveHarResult([FromBody]HarResultJsonDto json)
        {

            //var jsonSerializer = new JavaScriptSerializer();
            //var harResult = jsonSerializer.Deserialize<HarResultJsonDto>(json);
            try
            {

                HarManager manager = new HarManager();
                return manager.SaveHarResult(json).ToString();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}