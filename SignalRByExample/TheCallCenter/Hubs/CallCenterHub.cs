using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public class CallCenterHub : Hub<ICallCenterHub>
  {
    public async Task NewCallReceived(Call newCall)
    {
      await Clients.All.NewCallReceived(newCall);
    }
  }
}
