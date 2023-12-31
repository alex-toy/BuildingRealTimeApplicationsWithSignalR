﻿using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public interface ICallCenterHub
  {
    Task NewCallReceived(Call newCall);
    Task DeleteCallEvent(int id);
    Task ResolveCallEvent(int id);
    Task JoinCallCenters();
  }
}
