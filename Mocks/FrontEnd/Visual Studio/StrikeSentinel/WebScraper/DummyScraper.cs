using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace WebScraper
{
    class DummyScraper
    {
        public static string SITE_NAME = "Publico";
        public static string SITE_ADDRESS = @"https://www.publico.pt/ultimas/";

        public static List<string> Scrape() {

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
                foreach (var article in htmlNodesEachArticle) {
                    newsArticles.Add(article.InnerHtml);
                }
            }

            return newsArticles;
        }
    }
}
