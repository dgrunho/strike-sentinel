using System;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scraper is fetching news from " + WebScraper.DummyScraper.SITE_NAME + "...");
            foreach (string articleTitle in WebScraper.DummyScraper.Scrape())
            {
                Console.WriteLine("[" + articleTitle + "]");
            }
            Console.WriteLine("Done!!!");
            Console.ReadKey(true);
        }
    }
}
