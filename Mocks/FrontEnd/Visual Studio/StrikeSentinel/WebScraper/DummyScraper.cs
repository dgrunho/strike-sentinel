using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;

namespace WebScraper
{
    //TODO documentar classe
    public class DummyScraper
    {
        private struct News
        {
            public string Title { get; set; }
            public string Link { get; set; }
        }
        public IConfiguration Configuration { get; }

        public DummyScraper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region "Private Methods"

        //TODO documentar metodo
        private HtmlDocument ParseHtml(string siteAddress)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(siteAddress);
        }

        //TODO documentar metodo
        private List<News> TraversingHtml(string siteAddress, HtmlDocument htmlDoc, string xpathLastArticles, string xpathEachArticle)
        {
            List<News> newsArticles = new List<News>();
            News newsArticle;
            var htmlNodesLatestArticles = htmlDoc.DocumentNode.SelectNodes(xpathLastArticles);
            foreach (var articles in htmlNodesLatestArticles)
            {
                var htmlNodesEachArticle = articles.SelectNodes(xpathEachArticle);
                foreach (var article in htmlNodesEachArticle)
                {
                    newsArticle = new News();
                    newsArticle.Title = article.InnerHtml;
                    newsArticle.Link = siteAddress + article.GetAttributeValue("href","");
                    newsArticles.Add(newsArticle);
                }
            }
            return newsArticles;
        }

        //TODO documentar metodo
        private List<string> FilterResults(ref List<News> articlesList)
        {
            List<string> linksList = new List<string>();
            //obtem a lista com as keywords para pesquisa de greves, a partir do ficheiro de configuração
            IConfigurationSection strikeKeywordsSection = Configuration.GetSection("GeneralSettings").GetSection("StrikeKeywords");
            IEnumerable<IConfigurationSection> strikeKeywordsSectionMembers = strikeKeywordsSection.GetChildren();
            List<string> strikeKeywords = (from sk in strikeKeywordsSectionMembers select sk.Value).ToList();
            //pesquisa nos artigos recolhidos se algum contem alguma keyword relacionada com greves
            foreach (var article in articlesList)
            {
                foreach (string strikeKeyword in strikeKeywords)
                {
                    if (article.Title.ToUpper().Contains(strikeKeyword.ToUpper()))
                    {
                        linksList.Add(article.Link);
                    }
                }
            }
            return linksList;
        }

        //TODO documentar o metodo
        private void ValidateSettingsFile()
        {
            //TODO implementar
            throw new Exception("Method ValidateSettingsFile() not implemented yet!");
        }

        #endregion

        #region "Public Methods"

        //TODO documentar este metodo
        public List<string> ScrapeHtml()
        {
            //TODO ver o que fazer caso esta secção não esteja definida no ficheiro de configuração
            IConfigurationSection scrapersSettingsSection = Configuration.GetSection("ScraperSettings");
            IEnumerable<IConfigurationSection> scraperSettingsMembers = scrapersSettingsSection.GetChildren();
            string searchAddress, siteAddress, xpathLastArticles, xpathEachArticle;
            HtmlDocument htmlDoc;
            List<News> articlesList = new List<News>();
            List<News> articlesFromAllSourcesList = new List<News>();
            List<string> linksToArticlesList;

            foreach (IConfigurationSection scraperSetting in scraperSettingsMembers)
            {
                if(scraperSetting["State"].ToUpper() == "ON")
                { 
                    searchAddress = scraperSetting["SearchAddress"];
                    siteAddress = scraperSetting["SiteAddress"];
                    xpathLastArticles = scraperSetting.GetSection("XPath")["LastArticles"];
                    xpathEachArticle = scraperSetting.GetSection("XPath")["EachArticle"];
                
                    htmlDoc = ParseHtml(searchAddress);

                    articlesList = TraversingHtml(siteAddress, htmlDoc, xpathLastArticles, xpathEachArticle);
                    articlesFromAllSourcesList.AddRange(articlesList);
                }
            }

            linksToArticlesList = FilterResults(ref articlesFromAllSourcesList);

            return linksToArticlesList;
        }

        #endregion

        //TODO olhar para esta region e ver se já se pode apagar alguma coisa
        #region "Sucata (coisas para descontinuar brevemente...)"
        [Obsolete]
        public static string SITE_NAME = "Publico";
        [Obsolete]
        public static string SITE_ADDRESS = @"https://www.publico.pt/ultimas/";

        [Obsolete("NÃO USAR ESTE MÉTODO! a classe foi reformulada e só não removi (para já) este método para não partir o código")]
        public static List<string> Scrape()
        {

            List<string> newsArticles = new List<string>();

            string xpathLastArticles = "/html/body/div[1]/div[3]/main/div/div/section/div/div/div[2]/div/section[1]/ul/li[1]";
            string xpathEachArticle = "//article/div/div/div/h3/a";

            /* PARSE */
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(SITE_ADDRESS);

            /* TRAVERSING */
            var htmlNodesLatestArticles = htmlDoc.DocumentNode.SelectNodes(xpathLastArticles);
            foreach (var articles in htmlNodesLatestArticles)
            {
                var htmlNodesEachArticle = articles.SelectNodes(xpathEachArticle);
                foreach (var article in htmlNodesEachArticle)
                {
                    newsArticles.Add(article.InnerHtml);
                }
            }

            return newsArticles;
        }
        #endregion



    }
}
