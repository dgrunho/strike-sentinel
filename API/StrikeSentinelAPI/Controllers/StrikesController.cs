using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrikeSentinelAPI.Models;

namespace StrikeSentinelAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Strikes")]
    public class StrikesController : Controller
    {
        private readonly StrikeNewsContext _context;

        public StrikesController(StrikeNewsContext context)
        {
            _context = context;
        }

        // GET: api/Strikes
        [HttpGet]
        public IEnumerable<Strike> GetStrike()
        {
            return _context.Strike;
        }

        // GET: api/Strikes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrike([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strike = await _context.Strike.SingleOrDefaultAsync(m => m.StrikeId == id);

            if (strike == null)
            {
                return NotFound();
            }

            return Ok(strike);
        }

        // PUT: api/Strikes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrike([FromRoute] int id, [FromBody] Strike strike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != strike.StrikeId)
            {
                return BadRequest();
            }

            _context.Entry(strike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrikeExists(id))
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

        // POST: api/Strikes
        [HttpPost]
        public async Task<IActionResult> PostStrike([FromBody] Strike strike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Strike.Add(strike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrike", new { id = strike.StrikeId }, strike);
        }

        // DELETE: api/Strikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrike([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strike = await _context.Strike.SingleOrDefaultAsync(m => m.StrikeId == id);
            if (strike == null)
            {
                return NotFound();
            }

            _context.Strike.Remove(strike);
            await _context.SaveChangesAsync();

            return Ok(strike);
        }

        private bool StrikeExists(int id)
        {
            return _context.Strike.Any(e => e.StrikeId == id);
        }
    }
}