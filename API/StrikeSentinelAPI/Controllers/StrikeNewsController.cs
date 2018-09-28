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

        [HttpGet("Dummy")]
        public List<Greve> GetStrikeNewsDummy()
        {
            List<Greve> greves = new List<Greve>();

            DateTime Today = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            greves.Add(new Greve("1", "Comboios", Today, Today.AddDays(1).AddMilliseconds(-1), "bla bla", true, "check", "Confirmada", "Green", "CP", "http://google.com", "api/StrikeNews/Icon/1"));
            greves.Add(new Greve("2", "Metro", Today.AddDays(1), Today.AddDays(3).AddMilliseconds(-1), "bla bla", true, "cancel", "Cancelada", "Red", "Metro de Lisboa", "http://google.com", "api/StrikeNews/Icon/2"));
            greves.Add(new Greve("3", "Autocarro", Today.AddDays(2).AddHours(10), Today.AddDays(2).AddHours(14), "bla bla", false, "help", "A Confirmar", "GoldenRod", "Carris", "http://google.com", "api/StrikeNews/Icon/3"));
            greves.Add(new Greve("4", "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(7).AddHours(4), "bla bla", false, "help", "A Confirmar", "GoldenRod", "Centro Hospitalar do médio Tejo", "http://google.com", "api/StrikeNews/Icon/4"));
            greves.Add(new Greve("5", "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, "help", "A Confirmar", "GoldenRod", "Professores", "http://google.com", "api/StrikeNews/Icon/5"));
            greves.Add(new Greve("6", "Comboios", Today, Today.AddDays(1).AddMilliseconds(-1), "bla bla", true, "check", "Confirmada", "Green", "CP", "http://google.com", "api/StrikeNews/Icon/6"));
            greves.Add(new Greve("7", "Metro", Today.AddDays(1), Today.AddDays(3).AddMilliseconds(-1), "bla bla", true, "cancel", "Cancelada", "Red", "Metro de Lisboa", "http://google.com", "api/StrikeNews/Icon/7"));
            greves.Add(new Greve("8", "Autocarro", Today.AddDays(2).AddHours(10), Today.AddDays(2).AddHours(14), "bla bla", false, "help", "A Confirmar", "GoldenRod", "Carris", "http://google.com", "api/StrikeNews/Icon/8"));
            greves.Add(new Greve("9", "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(7).AddHours(4), "bla bla", false, "help", "A Confirmar", "GoldenRod", "Centro Hospitalar do médio Tejo", "http://google.com", "api/StrikeNews/Icon/9"));
            greves.Add(new Greve("10", "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, "help", "A Confirmar", "GoldenRod", "Professores", "http://google.com", "api/StrikeNews/Icon/10"));

            return greves;
        }

        [HttpGet("GroupDummy")]
        public List<GroupGreve> GetStrikeNewsGroupDummy()
        {
            List<Greve> greves = GetStrikeNewsDummy();
            List<GroupGreve> group_greves = new List<GroupGreve>();
            int id_group = 1;
            foreach (Greve greve in greves)
            {
                GroupGreve objGroup;
                objGroup = group_greves.Find(x => (x.Name == greve.DateGroup));
                if (objGroup == null)
                {
                    group_greves.Add(new GroupGreve(id_group.ToString(), greve.DateGroup, greve));
                    id_group += 1;
                } else
                {
                    objGroup.Greves.Add(greve);
                }
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