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
    [Route("api/StrikeStatus")]
    public class StrikeStatusController : Controller
    {
        private readonly StrikeNewsContext _context;

        public StrikeStatusController(StrikeNewsContext context)
        {
            _context = context;
        }

        // GET: api/StrikeStatus
        [HttpGet]
        public IEnumerable<StrikeStatus> GetStrikeStatus()
        {
            return _context.StrikeStatus;
        }

        // GET: api/StrikeStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrikeStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strikeStatus = await _context.StrikeStatus.SingleOrDefaultAsync(m => m.StrikeStatusId == id);

            if (strikeStatus == null)
            {
                return NotFound();
            }

            return Ok(strikeStatus);
        }

        // PUT: api/StrikeStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrikeStatus([FromRoute] int id, [FromBody] StrikeStatus strikeStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != strikeStatus.StrikeStatusId)
            {
                return BadRequest();
            }

            _context.Entry(strikeStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrikeStatusExists(id))
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

        // POST: api/StrikeStatus
        [HttpPost]
        public async Task<IActionResult> PostStrikeStatus([FromBody] StrikeStatus strikeStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StrikeStatus.Add(strikeStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrikeStatus", new { id = strikeStatus.StrikeStatusId }, strikeStatus);
        }

        // DELETE: api/StrikeStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrikeStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strikeStatus = await _context.StrikeStatus.SingleOrDefaultAsync(m => m.StrikeStatusId == id);
            if (strikeStatus == null)
            {
                return NotFound();
            }

            _context.StrikeStatus.Remove(strikeStatus);
            await _context.SaveChangesAsync();

            return Ok(strikeStatus);
        }

        private bool StrikeStatusExists(int id)
        {
            return _context.StrikeStatus.Any(e => e.StrikeStatusId == id);
        }
    }
}