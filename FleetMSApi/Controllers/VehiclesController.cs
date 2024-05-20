using FPro;
using Microsoft.AspNetCore.Mvc;
using FleetMSLogic.Manager;
using FleetMSLogic.Entities;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Collections.Concurrent;

namespace FleetMSApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";

                VehicleManager manager = new();

                Gvar = manager.GetAllVehicles();

                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult PostVehicle([FromBody] GVAR Gvar)
        {
            try
            {
                if (Gvar == null)
                {
                    return BadRequest($"Invalid input: Gvar is null. "+ Gvar);
                }

                //check input format
                if (!Gvar.DicOfDic.ContainsKey("Tags"))
                {
                    return BadRequest($"Tags dictionary is missing in GVAR object. {JsonConvert.SerializeObject(Gvar)}");
                }

                Gvar.DicOfDic["Tags"]["STS"] = "0";

                VehicleManager manager = new();
                manager.AddVehicle(Gvar);
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
                    return BadRequest($"Invalid input: Gvar is null. " + Gvar);
                }

                //check input format
                if (!Gvar.DicOfDic.ContainsKey("Tags"))
                {
                    return BadRequest($"Tags dictionary is missing in GVAR object. {JsonConvert.SerializeObject(Gvar)}");
                }

                Gvar.DicOfDic["Tags"]["STS"] = "0";


                VehicleManager manager = new();
                manager.UpdateVehicle(VehicleID, Gvar);
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to update : {ex.Message}");
            }

        }

        [HttpDelete("{VehicleID}")]
        public IActionResult DeleteVehicle(string VehicleID)
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";

                VehicleManager manager = new();
                manager.DeleteVehicle(VehicleID);

                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to delete : {ex.Message}");
            }
        }
    }
}
