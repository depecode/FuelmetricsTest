using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuelMetricsTest.Model;

namespace FuelMetricsTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParksController : ControllerBase
    {
        private readonly ParkDbContext _context;

        public ParksController(ParkDbContext context)
        {
            _context = context;
        }

        // GET: api/Parks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Park>>> GetParks()
        {
            return await _context.Parks.ToListAsync();
        }

        // GET: api/Parks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Park>> GetPark(int id)
        {
            var park = await _context.Parks.FindAsync(id);

            if (park == null)
            {
                return NotFound();
            }

            return park;
        }

        // PUT: api/Parks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPark(int id, Park park)
        {
            if (id != park.Id)
            {
                return BadRequest();
            }

            _context.Entry(park).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Parks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Park>> PostPark(Park park)
        {
            Park parktbl = new Park();

            if ((park.EntryTime != null) && (park.ExitTime != null))
            {
                decimal amountToPay = 0;

                TimeSpan duration = DateTime.Parse(park.ExitTime).Subtract(DateTime.Parse(park.EntryTime));

                int hours = (int)duration.TotalHours;
                int minutes = duration.Minutes;

                // int TotalDuration = hours + minutes;

                int time = hours; 

                if(time <= 1)
                {
                    amountToPay = 2 + 3;
                }
                else if(minutes != 0)
                {
                    amountToPay = 2 + 3 + ((time) * 4);
                }
                else
                {
                    amountToPay = 2 + 3 + ((time - 1) * 4);
                }


                parktbl.Name = park.Name;
                parktbl.EntryTime = park.EntryTime;
                parktbl.ExitTime = park.ExitTime;
                parktbl.HoursSpent = time.ToString();
                parktbl.AmountToPay = amountToPay;
                parktbl.Date = DateTime.Now;

            }

            


            _context.Parks.Add(parktbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPark", new { id = park.Id }, park);
        }

        // DELETE: api/Parks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Park>> DeletePark(int id)
        {
            var park = await _context.Parks.FindAsync(id);
            if (park == null)
            {
                return NotFound();
            }

            _context.Parks.Remove(park);
            await _context.SaveChangesAsync();

            return park;
        }

        //[HttpGet]
        //public List<Park> GetAllItems(
        //DateTime date,
        //int? page,
        //int limit = 5,
        //string dir = "desc")
        //{
        //    IQueryable<Park> query = _context.Parks;

        //    //if (!string.IsNullOrWhiteSpace(L))
        //    //    query = query.Where(d => d.Date.Contains(date));


        //    //query = query.Where(p => p.Date.Equals(date)).ToList();


        //    query = query.Take(limit);
        //    if (page.HasValue)
        //        query = query.Skip(page.Value * limit);

        //    return query.ToList();
        //}


        private bool ParkExists(int id)
        {
            return _context.Parks.Any(e => e.Id == id);
        }
    }
}
