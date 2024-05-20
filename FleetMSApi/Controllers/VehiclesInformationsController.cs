using FleetMSLogic.Manager;
using FleetMSLogic.Entities;
using FPro;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace FleetMSApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesInformationsController : Controller
    {
        [HttpGet("{VehicleID}")]
        public IActionResult GetVehicleInformation(string VehicleID){
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";
                VehicleManager manager = new();

                Gvar = manager.GetVehicleInformations(VehicleID);
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult PostVehicleInformations([FromBody] GVAR Gvar)
        {
            try
            {
                if (Gvar == null)
                {
                    return BadRequest($"Invalid input: Gvar is null. { JsonConvert.SerializeObject(Gvar)}");
                }

                //check input format
                if (!Gvar.DicOfDic.ContainsKey("Tags"))
                {
                    return BadRequest($"Tags dictionary is missing in GVAR object. {JsonConvert.SerializeObject(Gvar)}");
                }

                Gvar.DicOfDic["Tags"]["STS"] = "0";

                VehicleManager manager = new();
                manager.AddVehicleInformations(Gvar);
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));

            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to add : {ex.Message}");
            }

        }

        [HttpPut("{VehicleID}")]
        public IActionResult UpdateVehicle(string VehicleID, [FromBody] GVAR Gvar)
        {
            try
            {
                if (Gvar == null)
                {
                    return BadRequest($"Invalid input: Gvar is null. {JsonConvert.SerializeObject(Gvar)}");
                }

                //check input format
                if (!Gvar.DicOfDic.ContainsKey("Tags"))
                {
                    return BadRequest($"Tags dictionary is missing in GVAR object. {JsonConvert.SerializeObject(Gvar)}");
                }

                Gvar.DicOfDic["Tags"]["STS"] = "0";

                VehicleManager manager = new();
                manager.UpdateVehicleInformations(VehicleID, Gvar);
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to update : {ex.Message}");
            }

        }
    }
}
