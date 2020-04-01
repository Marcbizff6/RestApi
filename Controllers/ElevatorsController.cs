using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using Newtonsoft.Json.Linq;

namespace RestApi.Controllers
{
  [Route("api/elevators")]
  [ApiController]
  public class ElevatorsController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public ElevatorsController(DatabaseContext context)
    {
      _context = context;
    }

    [HttpGet("get/status/all")]
    public async Task<ActionResult<IEnumerable<Elevators>>> GetElevators()
    {

      return await _context.Elevators.ToListAsync();
    }

    [HttpGet("get/status/inactive")]
    public IEnumerable<Elevators> GetElevatorsInactive()
    {
      IQueryable<Elevators> Elevators =
      from elev in _context.Elevators
      where elev.Status == "Inactive"
      select elev;

      return Elevators.ToList();
    }


    [HttpGet("get/status/active")]
    public IEnumerable<Elevators> GetElevatorsActive()
    {
      IQueryable<Elevators> Elevators =
      from elev in _context.Elevators
      where elev.Status == "Active"
      select elev;

      return Elevators.ToList();
    }

    [HttpGet("get/status/intervention")]
    public IEnumerable<Elevators> GetElevatorsIntervention()
    {
      IQueryable<Elevators> Elevators =
      from elev in _context.Elevators
      where elev.Status != "Active" && elev.Status != "Inactive"
      select elev;

      return Elevators.ToList();
    }

    //https://stackoverflow.com/questions/9777731/mvc-how-to-return-a-string-as-json
    //https://stackoverflow.com/questions/16459155/how-to-access-json-object-in-c-sharp

    [HttpGet("get/status/{id}")]
    public async Task<ActionResult<IEnumerable<Elevators>>> GetElevators(long id, string Status)
    {
      var Elevators = await _context.Elevators.FindAsync(id);

      if (Elevators == null)
      {
        return NotFound();
      }

      var jsonGet = new JObject();
      jsonGet["id"] = Elevators.Id;
      jsonGet["status"] = Elevators.Status;
      return Content(jsonGet.ToString(), "application/json");
    }

    [HttpPut("put/status/activate/{id}")]
    public async Task<IActionResult> PutElevatorsStatusActive(long id, Elevators Elevators)
    {
      if (id != Elevators.Id)
      {
        return BadRequest();
      }

      Elevators.Status = "Active";
      _context.Entry(Elevators).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ElevatorsExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Content("Elevator: " + Elevators.Id + ", status as been change to: " + Elevators.Status);
    }

    [HttpPut("put/status/inactivate/{id}")]
    public async Task<IActionResult> PutElevatorsStatusInactive(long id, Elevators Elevators)
    {
      if (id != Elevators.Id)
      {
        return BadRequest();
      }

      Elevators.Status = "Inactive";
      _context.Entry(Elevators).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ElevatorsExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Content("Elevator: " + Elevators.Id + ", status as been change to: " + Elevators.Status);
    }

    [HttpPut("put/status/intervention/{id}")]
    public async Task<IActionResult> PutElevatorsStatusIntervention(long id, Elevators Elevators)
    {
      if (id != Elevators.Id)
      {
        return BadRequest();
      }

      Elevators.Status = "Intervention";
      _context.Entry(Elevators).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ElevatorsExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Content("Elevator: " + Elevators.Id + ", status as been change to: " + Elevators.Status);
    }

    [HttpPost]
    public async Task<ActionResult<Elevators>> PostElevators(Elevators Elevators)
    {
      _context.Elevators.Add(Elevators);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetElevators", new { id = Elevators.Id }, Elevators);
    }

    // DELETE: api/Elevators/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Elevators>> DeleteElevators(long id)
    {
      var Elevators = await _context.Elevators.FindAsync(id);
      if (Elevators == null)
      {
        return NotFound();
      }

      _context.Elevators.Remove(Elevators);
      await _context.SaveChangesAsync();

      return Elevators;
    }

    private bool ElevatorsExists(long id)
    {
      return _context.Elevators.Any(e => e.Id == id);
    }
  }
}
