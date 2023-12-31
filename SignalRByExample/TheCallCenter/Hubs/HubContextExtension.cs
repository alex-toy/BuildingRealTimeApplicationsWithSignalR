﻿using Microsoft.AspNetCore.SignalR;
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

    public static async Task AlertGroupOnDeleteCallEvent(this IHubContext<CallCenterHub, ICallCenterHub> _hubContext, int id)
    {
      await _hubContext.Clients.Group("CallCenter").DeleteCallEvent(id);
    }

    public static async Task AlertGroupOnResolveCallEvent(this IHubContext<CallCenterHub, ICallCenterHub> _hubContext, int id)
    {
      await _hubContext.Clients.Group("CallCenter").ResolveCallEvent(id);
    }
  }
}
