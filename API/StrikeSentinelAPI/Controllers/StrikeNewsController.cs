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

        #region "Actions"

        [HttpPut("SearchStrikeNews")]
        public IActionResult SearchStrikeNews()
        {
            IConfiguration botContiguration;
            try
            {
                botContiguration = GetConfiguration("scrapersettings.json");
            }
            catch (Exception)
            {
                //TODO log do que aconteceu de errado
                throw;
            }

            //pesquisa novas noticias
            DummyScraper bot = new DummyScraper(botContiguration);
            List<String> linksList = bot.ScrapeHtml();
            StrikeNews strikeNews;

            foreach (string link in linksList)
            {
                if (!StrikeNewsExists(link))
                { 
                    strikeNews = new StrikeNews();
                    strikeNews.SourceLink = link;
                    _context.StrikeNews.Add(strikeNews);
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        #endregion

        #region "CRUD Operations"

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
            greves.Add(new Greve("1", "Comboios", Today, Today.AddDays(1).AddMilliseconds(-1), "bla bla", true, "Check", "Confirmada", "Green", "CP", "http://google.com"));
            greves.Add(new Greve("2", "Metro", Today.AddDays(1), Today.AddDays(3).AddMilliseconds(-1), "bla bla", true, "Cancel", "Cancelada", "Red", "Metro de Lisboa", "http://google.com"));
            greves.Add(new Greve("3", "Autocarro", Today.AddDays(2).AddHours(10), Today.AddDays(2).AddHours(14), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Carris", "http://google.com"));
            greves.Add(new Greve("4", "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(7).AddHours(4), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Centro Hospitalar do médio Tejo", "http://google.com"));
            greves.Add(new Greve("5", "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, "Help", "A Confirmar", "GoldenRod", "Professores", "http://google.com"));
            greves.Add(new Greve("6", "Comboios", Today, Today.AddDays(1).AddMilliseconds(-1), "bla bla", true, "Check", "Confirmada", "Green", "CP", "http://google.com"));
            greves.Add(new Greve("7", "Metro", Today.AddDays(1), Today.AddDays(3).AddMilliseconds(-1), "bla bla", true, "Cancel", "Cancelada", "Red", "Metro de Lisboa", "http://google.com"));
            greves.Add(new Greve("8", "Autocarro", Today.AddDays(2).AddHours(10), Today.AddDays(2).AddHours(14), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Carris", "http://google.com"));
            greves.Add(new Greve("9", "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(7).AddHours(4), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Centro Hospitalar do médio Tejo", "http://google.com"));
            greves.Add(new Greve("10", "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, "Help", "A Confirmar", "GoldenRod", "Professores", "http://google.com"));

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

            return group_greves;
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

        // GET: api/StrikeNews/Icon/5
        [HttpGet("Icon/{id}")]
        public async Task<IActionResult> GetIcon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var webRoot = _env.ContentRootPath;
            var file = "";
            

            switch (id)
{
                case 1:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\CP.png");
                    break;
                case 2:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Metro.png");
                    break;
                case 3:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Carris.jpg");
                    break;
                case 4:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\CHMT.png");
                    break;
                case 5:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Professores.png");
                    break;
                case 6:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\CP.png");
                    break;
                case 7:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Metro.png");
                    break;
                case 8:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Carris.jpg");
                    break;
                case 9:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\CHMT.png");
                    break;
                case 10:
                    file = System.IO.Path.Combine(webRoot, "Images\\IconesEmpresas\\Professor.png");
                    break;
                default:
                    file = System.IO.Path.Combine(webRoot, "Images\\web_hi_res_512.png");
                    break;
            }

            var image = System.IO.File.OpenRead(file);
            return await Task.Run(() => File(image, "image/png"));
        }

        // GET: api/StrikeNews/Path/5
        [HttpGet("Ico")]
        public async Task<string> GetPath()
        {
            var webRoot = _env.ContentRootPath;
            return webRoot;

            //var strikeNews = await _context.StrikeNews.SingleOrDefaultAsync(m => m.StrikeNewsId == id);

            //if (strikeNews == null)
            //{
            //    return NotFound();
            //}

            //return Ok(strikeNews);
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

        #endregion

        private bool StrikeNewsExists(int id)
        {
            return _context.StrikeNews.Any(e => e.StrikeNewsId == id);
        }

        private bool StrikeNewsExists(string sourceLink)
        {
            return _context.StrikeNews.Any(e => e.SourceLink == sourceLink);
        }

        private IConfiguration GetConfiguration(string fileName)
        {
            
            var builder = new ConfigurationBuilder()
             .SetBasePath(_env.ContentRootPath)
             .AddJsonFile(fileName, optional: true);

            //TODO verificar se foram recolhidas algumas configuracoes

            return builder.Build();
        }

        //TODO olhar para esta region e ver se já se pode apagar alguma coisa
        #region "Sucata (coisas para descontinuar brevemente...)"

        // GET: api/StrikeNews/search
        [Obsolete]
        [HttpGet("{command}")]
        public string SearchStrikeNews([FromRoute] String command)
        {
            try
            {
                if (command == "search")
                {
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
            }
            catch (Exception e)
            {
                return "Error: " + e.ToString();
            }
        }

        #endregion

    }
}