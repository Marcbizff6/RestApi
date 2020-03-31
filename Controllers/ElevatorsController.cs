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

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevators>>> GetElevators()
        {
            return await _context.Elevators.ToListAsync();
        }

        // GET: api/Elevators/5



 //https://stackoverflow.com/questions/9777731/mvc-how-to-return-a-string-as-json
 //https://stackoverflow.com/questions/16459155/how-to-access-json-object-in-c-sharp

        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> GetElevators(long id, string Status)
        {
            var Elevators = await _context.Elevators.FindAsync(id);

            if (Elevators == null)
            {
                return NotFound();
            }

            var jsonGet = new JObject ();
            jsonGet["status"] = Elevators.Status;
            return Content  (jsonGet.ToString(), "application/json");
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevators(long id, Elevators Elevators)
        {
            if (id != Elevators.Id)
            {
                return BadRequest();
            }

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

            var jsonPut = new JObject ();
            jsonPut["Update"] = "Update done to elevator id : " + id;
            return Content  (jsonPut.ToString(), "application/json");

        }

        // POST: api/Elevators
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
