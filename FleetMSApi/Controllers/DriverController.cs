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
    public class DriverController : Controller
    {
        [HttpGet]
        public IActionResult GetAllDrivers()
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";
                DriverManager manager = new();

                Gvar = manager.GetAllDrivers();
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
        public IActionResult PostDriver([FromBody] GVAR Gvar)
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

                DriverManager manager = new();
                manager.AddDriver(Gvar);
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
                
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to add : {ex.Message}");
            }

        }

        [HttpPut("{DriverID}")]
        public IActionResult UpdateDriver(string DriverID, [FromBody] GVAR Gvar)
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

                DriverManager manager = new();
                manager.UpdateDriver(DriverID, Gvar);
                Gvar.DicOfDic["Tags"]["STS"] = "0";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to update : {ex.Message}");
            }

        }

        [HttpDelete("{DriverID}")]
        public IActionResult DeleteDriver(string DriverID)
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";

                DriverManager manager = new();
                manager.DeleteDriver(DriverID);
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
