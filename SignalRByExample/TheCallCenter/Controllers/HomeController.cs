using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TheCallCenter.Data;
using TheCallCenter.Data.Entities;
using TheCallCenter.Hubs;
using TheCallCenter.Models;

namespace TheCallCenter.Controllers
{
  public class HomeController : Controller
  {
    private readonly CallCenterContext _callCenterContext;
    private readonly IHubContext<CallCenterHub> _hubContext;

    public HomeController(CallCenterContext ctx, IHubContext<CallCenterHub> hubContext)
    {
      _callCenterContext = ctx;
      _hubContext = hubContext;
    }

    public IActionResult Index()
    {
      ViewBag.Message = "";
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(Call call)
    {
      try
      {
        if (!ModelState.IsValid) return View();

        _callCenterContext.Add(call);
        if (await _callCenterContext.SaveChangesAsync() > 0)
        {
          ViewBag.Message = "Problem Reported...";
          ModelState.Clear();

          await AlertAllClients(call);
        }
        else
        {
          ViewBag.Message = "Failed to save new problem...";
        }
      }
      catch (Exception)
      {
        ViewBag.Message = "Threw exception trying to save call";
      }

      return View();
    }

    private async Task AlertAllClients(Call call)
    {
      await _hubContext.Clients.All.SendAsync("NewCallReceived", call);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Calls()
    {
      return View();
    }
  }
}
