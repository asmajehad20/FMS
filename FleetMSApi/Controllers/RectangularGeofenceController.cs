using FleetMSLogic.Manager;
using FleetMSLogic.Entities;
using FPro;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace FleetMSApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RectangularGeofenceController : Controller
    {
        [HttpGet]
        public IActionResult GetAllRectangularGeofences()
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";
                GeofenceManager manager = new();

                Gvar = manager.GetAllRectangularGeofence();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
