using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;

namespace WebScraper
{
    //TODO documentar classe
    public class DummyScraper
    {
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
        private List<string> TraversingHtml(HtmlDocument htmlDoc, string xpathLastArticles, string xpathEachArticle)
        {
            List<string> newsArticles = new List<string>();
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

        #region "Public Methods"

        public List<string> ScrapeHtml()
        {
            //TODO ver o que fazer caso esta secção não esteja definida no ficheiro de configuração
            IConfigurationSection publicoConfiguratio = Configuration.GetSection("PublicoScraper");
            string siteAddress = publicoConfiguratio["SiteAddress"];
            string xpathLastArticles = publicoConfiguratio.GetSection("XPath")["LastArticles"];
            string xpathEachArticle = publicoConfiguratio.GetSection("XPath")["EachArticle"];

            HtmlDocument htmlDoc;
            htmlDoc = ParseHtml(siteAddress);

            List<string> result;
            result = TraversingHtml(htmlDoc,xpathLastArticles,xpathEachArticle);

            return result;
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
