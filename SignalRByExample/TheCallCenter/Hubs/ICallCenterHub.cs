using System.Threading.Tasks;
using TheCallCenter.Data.Entities;

namespace TheCallCenter.Hubs
{
  public interface ICallCenterHub
  {
    Task NewCallReceived(Call newCall);
    Task JoinCallCenters();
  }
}