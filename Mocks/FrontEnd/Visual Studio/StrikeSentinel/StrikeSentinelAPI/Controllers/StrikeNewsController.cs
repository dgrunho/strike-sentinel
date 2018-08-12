using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrikeSentinelAPI.Models;
using WebScraper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace StrikeSentinelAPI.Controllers
{
    //TODO apagar esta classe
    public class APICommand {
        public string command { get; set; }
        public IEnumerable<string> parameters { get; set; }
    }

    [Produces("application/json")]
    [Route("api/StrikeNews")]
    public class StrikeNewsController : Controller
    {
        private readonly StrikeNewsContext _context;
        private readonly IHostingEnvironment _env;

        public StrikeNewsController(IHostingEnvironment hostingEnvironment, StrikeNewsContext context)
        {
            _env = hostingEnvironment;
            _context = context;
        }

        /*
         * Instruções à API
         */
        // GET: api/StrikeNews/search
        [HttpGet("{command}")]
        public string SearchStrikeNews([FromRoute] String command)
        {
            try {
                if(command == "search") { 
                    List<String> news = DummyScraper.Scrape();
                    StrikeNews strikeNews;
                    foreach (string title in news)
                    {
                        strikeNews = new StrikeNews();
                        strikeNews.SourceLink = title;
                        _context.StrikeNews.Add(strikeNews);
                        _context.SaveChanges();
                    }
                }
                if (command == "searchv2")
                {
                    DummyScraper bot = new DummyScraper(GetConfiguration("scrapersettings.json"));
                    List<String> news = bot.ScrapeHtml();
                    StrikeNews strikeNews;
                    foreach (string title in news)
                    {
                        strikeNews = new StrikeNews();
                        strikeNews.SourceLink = title;
                        _context.StrikeNews.Add(strikeNews);
                        _context.SaveChanges();
                    }
                }
                return command;
            } catch (Exception e)
            {
                return "Error: " + e.ToString();
            }
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

        private IConfiguration GetConfiguration(string fileName)
        {
            
            var builder = new ConfigurationBuilder()
             .SetBasePath(_env.ContentRootPath)
             .AddJsonFile(fileName, optional: true);

            //TODO verificar se foram recolhidas algumas configuracoes

            return builder.Build();
        }

    }
}