using HtmlAgilityPack;
using System.Collections.Generic;

namespace webscraper
{
    public class LeagueTeams
    {
        public string LeagueName { get; set; }
        public List<Team> Teams;
        public LeagueTeams()
        {
            Teams = new List<Team>();
        }
        public LeagueTeams(string league) : this()
        {
            LeagueName = league;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.bbc.com/sport/football/" + league + "/table");

            HtmlNode legaueTable = doc.DocumentNode.SelectSingleNode("//table[@data-competition-slug='" + league + "']");
            HtmlNodeCollection teamRows = legaueTable.SelectNodes("//tr[@data-team-slug]");
            foreach (HtmlNode team in teamRows)
            {
                Teams.Add(new Team(team));
            }
        }
    }

}
