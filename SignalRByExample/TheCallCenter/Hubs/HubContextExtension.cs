using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public static class HubContextExtension
  {

    public static async Task AlertAllClients(this IHubContext<CallCenterHub, ICallCenterHub> _hubContext, Call call)
    {
      await _hubContext.Clients.All.NewCallReceived(call);
    }

    public static async Task AlertGroup(this IHubContext<CallCenterHub, ICallCenterHub> _hubContext, Call call)
    {
      await _hubContext.Clients.Group("CallCenter").NewCallReceived(call);
    }
  }
}
