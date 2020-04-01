using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using Newtonsoft.Json.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
//using Microsoft.Ajax.Utilities;
//using RestApi.Controllers;

namespace RestApi.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BuildingsController(DatabaseContext context)
        {
            _context = context;
        }





//https://forums.asp.net/t/2154224.aspx?+NET+Core+C+Table+Joins+and+Query+Strings

//https://stackoverflow.com/questions/56978628/return-an-iqueryable-that-joins-two-tables

// public IQueryable<Vehicle> GetVehicles()
// {

//         from b in _context.Vehicles
//         join si in _context.ServiceIntervals on v.Id equals si.VehicleId 
//          join st in _context.ServiceMec on si.Id equals st.Id 
//         where
//             v.Schedule == true
//             && v.Suspend == false
//         select b;

// }


    [HttpGet("intervention")]

    public  ActionResult<List<Buildings>> GetBuildingsWithProblems()
        {

        IQueryable<Buildings>  Building = 

        from bui in _context.Buildings
        join bat in _context.Batteries on bui.id equals bat.building_id
        join col in _context.Columns on bat.id equals col.battery_id
        join ele in _context.Elevators on col.id equals ele.column_id

        where bat.status == "Intervention" || col.status == "Intervention" || ele.status == "Intervention" // va tu les ajouter 3 fois sans specifier si col, ele ou bat ?
        select bui;
        
        // Building.ToList();

        // List<Buildings> duplicateBuildings = Building.Except(Building.GroupBy(i => i.id)
        //                                       .Select(ss => ss.FirstOrDefault()))
        //                                      .ToList();

      //  var query = Building.GroupBy(x => x.id).Select(y => y.FirstOrDefault());
   //  Building.ToList();

   //   var listToReturn = Building.DistinctBy(x => x.id).ToList();
    //  var result = Building.Where(p => p.id != null).GroupBy(p => p.id).Select(grp => grp.FirstOrDefault());

    //  var distinctItems = Building.GroupBy(x => x.id).Select(y => y.First());

     // var distinctItems =  Building.Select(x => x.id).Distinct();
      
       return Building.ToList();




}


//  [HttpGet("inactive")]
//         public  ActionResult<List<Buildings>> GetBuildingsWithProblems()
//         {

//              var list =  _context.Buildings;
//          

//              if (list == null)
//             {
//                 return NotFound();
//             }

//             List<Buildings> listBuildingsIntervenation = new List<Buildings>();



//             var panic = false;

//             foreach (var building in list){

//                 foreach (var battery in building.Batteries) {
//                         if (battery.Status == "Inactive") {
                            
//                             panic = true;

//                         }
//                     foreach (var column in battery.Columns){
//                          if (column.Status == "Inactive") {
                            
//                             panic = true;

//                         }   
                        
//                         foreach (var elevator in column.Elevators){
//                          if (elevator.Status == "Inactive") {
                            
//                             panic = true;

//                         }

//                     }

//                 }
//                 }
//                 if (panic == true){
//                 listBuildingsIntervenation.Add(building);
//                 }
//             }
//              return listBuildingsIntervenation;

//             }
        


    


        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(long id, string Status)
        {
            var Buildings = await _context.Buildings.FindAsync(id);

            if (Buildings == null)
            {
                return NotFound();
            }

    
            return Buildings;
        
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(long id, Buildings Buildings)
        {
            if (id != Buildings.id)
            {
                return BadRequest();
            }

            _context.Entry(Buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            var jsonPut = new JObject ();
            jsonPut["Update"] = "Update done to Buildings id : " + id;
            return Content  (jsonPut.ToString(), "application/json");

        }

        // POST: api/Buildings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Buildings>> PostBuildings(Buildings Buildings)
        {
            _context.Buildings.Add(Buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = Buildings.id }, Buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buildings>> DeleteBuildings(long id)
        {
            var Buildings = await _context.Buildings.FindAsync(id);
            if (Buildings == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(Buildings);
            await _context.SaveChangesAsync();

            return Buildings;
        }

        private bool BuildingsExists(long id)
        {
            return _context.Buildings.Any(e => e.id == id);
        }
    }
}
