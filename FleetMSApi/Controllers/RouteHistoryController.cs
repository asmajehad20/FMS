﻿using FPro;
using Microsoft.AspNetCore.Mvc;
using FleetMSLogic.Manager;
using FleetMSLogic.Entities;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace FleetMSApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RouteHistoryController : ControllerBase
    {
        private readonly IHubContext<UpdateHub> _hubContext;
        public RouteHistoryController(IHubContext<UpdateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("{VehicleID}")]
        public IActionResult GetRouteHistoryController(string VehicleID, [FromHeader]string StartTime, [FromHeader]string EndTime)
        {
            try
            {
                GVAR Gvar = new();
                Gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                Gvar.DicOfDic["Tags"]["STS"] = "0";
                VehicleManager manager = new();

                Gvar = manager.GetVehicleRouteHistory(VehicleID, StartTime, EndTime);
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
        public async Task<IActionResult> PostRouteHistory([FromBody] GVAR Gvar)
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
                manager.AddRouteHistory(Gvar);

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonConvert.SerializeObject(manager.GetAllVehicles()));
                Gvar.DicOfDic["Tags"]["STS"] = "1";
                return Ok(JsonConvert.SerializeObject(Gvar));
            }
            catch (Exception ex)
            {
                return BadRequest($"ERROR: failed to add : {ex.Message}");
            }

        }


    }
}