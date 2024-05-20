using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using FPro;
using Newtonsoft.Json;
namespace FleetMSApi
{
    public class UpdateHub : Hub
    {
        public async Task SendUpdate(GVAR gvar)
        {
            await Clients.All.SendAsync("UpdateData", JsonConvert.SerializeObject(gvar));
        }
    }

}
