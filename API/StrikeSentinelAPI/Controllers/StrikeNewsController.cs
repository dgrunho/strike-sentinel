﻿using System;
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
    [Route("api/StrikeNews")]
    public class StrikeNewsController : Controller
    {
        private readonly StrikeNewsContext _context;

        public StrikeNewsController(StrikeNewsContext context)
        {
            _context = context;
        }

        // GET: api/StrikeNews
        [HttpGet]
        public IEnumerable<StrikeNews> GetStrikeNews()
        {
            return _context.StrikeNews;
        }

        // GET: api/StrikeNews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrikeNews([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strikeNews = await _context.StrikeNews.SingleOrDefaultAsync(m => m.StrikeNewsId == id);

            if (strikeNews == null)
            {
                return NotFound();
            }

            return Ok(strikeNews);
        }

        // PUT: api/StrikeNews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrikeNews([FromRoute] int id, [FromBody] StrikeNews strikeNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != strikeNews.StrikeNewsId)
            {
                return BadRequest();
            }

            _context.Entry(strikeNews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrikeNewsExists(id))
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

        // POST: api/StrikeNews
        [HttpPost]
        public async Task<IActionResult> PostStrikeNews([FromBody] StrikeNews strikeNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StrikeNews.Add(strikeNews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrikeNews", new { id = strikeNews.StrikeNewsId }, strikeNews);
        }

        // DELETE: api/StrikeNews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrikeNews([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strikeNews = await _context.StrikeNews.SingleOrDefaultAsync(m => m.StrikeNewsId == id);
            if (strikeNews == null)
            {
                return NotFound();
            }

            _context.StrikeNews.Remove(strikeNews);
            await _context.SaveChangesAsync();

            return Ok(strikeNews);
        }

        private bool StrikeNewsExists(int id)
        {
            return _context.StrikeNews.Any(e => e.StrikeNewsId == id);
        }
    }
}