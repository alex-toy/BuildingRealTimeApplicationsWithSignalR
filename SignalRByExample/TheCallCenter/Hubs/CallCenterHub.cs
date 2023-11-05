using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public class CallCenterHub : Hub<ICallCenterHub>
  {
    public async Task NewCallReceived(Call newCall)
    {
      //await Clients.All.NewCallReceived(newCall);
      await Clients.Group("CallCenter").NewCallReceived(newCall);
    }
    public async Task DeleteCallEvent(int id)
    {
      await Clients.Group("CallCenter").DeleteCallEvent(id);
    }

    public async Task JoinCallCenters()
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, "CallCenter");
    }
  }
}
