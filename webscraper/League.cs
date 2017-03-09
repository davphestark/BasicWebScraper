using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace webscraper
{
    public class League
    {
        public string LeagueId { get; set; }
        public List<Team> Teams;
        public List<LeagueDivisions> Divisions;
        public bool IsTopOfPyramid;
        public string LeagueName { get; set; }
        public League()
        {
            Teams = new List<Team>();
            Divisions = new List<LeagueDivisions>();
        }
        public void Init(string league)
        {
            LeagueId = league;
            if (league.Contains("premier")) { IsTopOfPyramid = true; }
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.bbc.com/sport/football/" + league + "/table");

            HtmlNode legaueTable = doc.DocumentNode.SelectSingleNode("//table[@data-competition-slug='" + league + "']");
            HtmlNodeCollection teamRows = legaueTable.SelectNodes("tbody/tr[@data-team-slug]");
            foreach (HtmlNode team in teamRows)
            {
                var t = new Team();
                t.Init(team);
                Teams.Add(t);
            }
            var leagueDivisionGuesser = new LeagueDivisionGuesser();
            leagueDivisionGuesser.Init(this);
            leagueDivisionGuesser.GuessDivisionsFromDividers();
            
        }
        

    }

}
