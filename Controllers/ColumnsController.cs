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
    [Route("api/columns")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ColumnsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> GetColumns()
        {
            return await _context.Columns.ToListAsync();
        }

        // GET: api/Columns/5



 //https://stackoverflow.com/questions/9777731/mvc-how-to-return-a-string-as-json
 //https://stackoverflow.com/questions/16459155/how-to-access-json-object-in-c-sharp

        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> GetColumns(long id, string Status)
        {
            var Columns = await _context.Columns.FindAsync(id);

            if (Columns == null)
            {
                return NotFound();
            }

            var jsonGet = new JObject ();
            jsonGet["status"] = Columns.status;
            return Content  (jsonGet.ToString(), "application/json");
        }

        // PUT: api/Columns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumns(long id, Columns Columns)
        {
            if (id != Columns.id)
            {
                return BadRequest();
            }

            _context.Entry(Columns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var jsonPut = new JObject ();
            jsonPut["Update"] = "Update done to column id : " + id;
            return Content  (jsonPut.ToString(), "application/json");

        }

        // POST: api/Columns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Columns>> PostColumns(Columns Columns)
        {
            _context.Columns.Add(Columns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumns", new { id = Columns.id }, Columns);
        }

        // DELETE: api/Columns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Columns>> DeleteColumns(long id)
        {
            var Columns = await _context.Columns.FindAsync(id);
            if (Columns == null)
            {
                return NotFound();
            }

            _context.Columns.Remove(Columns);
            await _context.SaveChangesAsync();

            return Columns;
        }

        private bool ColumnsExists(long id)
        {
            return _context.Columns.Any(e => e.id == id);
        }
    }
}
