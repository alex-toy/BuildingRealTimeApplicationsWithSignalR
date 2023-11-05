using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TheCallCenter.Data;
using TheCallCenter.Hubs;

namespace TheCallCenter.Controllers
{
  [ApiController]
  [Route("api/calls")]
  public class CallsController : Controller
  {
    private readonly CallCenterContext _callCenterContext;
    private readonly IHubContext<CallCenterHub, ICallCenterHub> _hubContext;

    public CallsController(CallCenterContext ctx, IHubContext<CallCenterHub, ICallCenterHub> hubContext)
    {
      _callCenterContext = ctx;
      _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var calls = await _callCenterContext.Calls.ToListAsync();

      return Ok(calls);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var call = await _callCenterContext.Calls.Where(c => c.Id == id).FirstOrDefaultAsync();
        if (call == null) return BadRequest();

        _callCenterContext.Remove(call);
        if (await _callCenterContext.SaveChangesAsync() > 0)
        {
          await _hubContext.AlertGroupOnDeleteCallEvent(id);
          return Ok(new { success = true });
        }
        else
        {
          return BadRequest("Database Error");
        }
      }
      catch
      {
        return StatusCode(500);
      }
    }

    [HttpPost("resolve/{id:int}")]
    public async Task<IActionResult> Resolve(int id)
    {
      try
      {
        var call = await _callCenterContext.Calls.Where(c => c.Id == id).FirstOrDefaultAsync();
        if (call == null) return BadRequest();

        //_callCenterContext.Resolve(call);
        if (await _callCenterContext.SaveChangesAsync() > 0 || true)
        {
          await _hubContext.AlertGroupOnResolveCallEvent(id);
          return Ok(new { success = true });
        }
        else
        {
          return BadRequest("Database Error");
        }
      }
      catch
      {
        return StatusCode(500);
      }
    }
  }
}
