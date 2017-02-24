using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.bbc.com/sport/football/premier-league/table");
            
            HtmlNode legaueTable = doc.DocumentNode.SelectSingleNode("//table[@data-competition-slug='premier-league']");
            if (legaueTable != null)
            {
                int leaderPoints = 0;
                int champsPoints = 0;
                int UnitedPoints = 0;
                HtmlNodeCollection teamRows = legaueTable.SelectNodes("//tr[@data-team-slug]");
                HtmlNode leaders = legaueTable.SelectSingleNode("//tr//td[@class='position']/span[text() = 1 and @class='position-number']");
                leaderPoints = int.Parse(leaders.ParentNode.SelectSingleNode("//td[@class='points']").InnerText);
                HtmlNode fourth = legaueTable.SelectSingleNode("//tr//td[@class='position']/span[text() = 4 and @class='position-number']");
                champsPoints = int.Parse(fourth.ParentNode.ParentNode.SelectSingleNode("td[@class='points']").InnerText);
                Console.WriteLine("League leaders are at: " + leaderPoints + " points");
                Console.WriteLine("Final Champions League place is at: " + champsPoints + " points");

                HtmlNode United = legaueTable.SelectSingleNode("//tr[@data-team-slug='manchester-united']");
                UnitedPoints = int.Parse(United.SelectSingleNode("td[@class='points']").InnerText);
                Console.WriteLine(United.SelectSingleNode("td[@class='team-name']").InnerText + " are " + (leaderPoints - UnitedPoints) + " behind the leaders");
                Console.WriteLine(United.SelectSingleNode("td[@class='team-name']").InnerText + " are " + (champsPoints - UnitedPoints) + " back of the champions league");
                Console.WriteLine("the last ten games for " + United.SelectSingleNode("td[@class='team-name']").InnerText + ":");
                HtmlNodeCollection games = United.SelectNodes("td[@class='last-10-games']/ol/li");
                foreach (HtmlNode game in games)
                {
                    Console.WriteLine(game.GetAttributeValue("title", "no game found"));
                }

            }
            else { Console.WriteLine("Didn't find the table"); }

            Console.ReadKey();
        }
    }
}
