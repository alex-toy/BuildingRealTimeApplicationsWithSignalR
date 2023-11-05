using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public class CallCenterHub : Hub
  {
    public async Task NewCallReceived(Call newCall)
    {
      await Clients.All.SendAsync("NewCallReceived", newCall);
    }
  }
}
