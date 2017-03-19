using HtmlAgilityPack;
using System;
using System.Collections.Generic;

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
            IsTopOfPyramid = IsTopLeague(league);
            HtmlNode leagueTable = LoadLeagueTable();
            if (leagueTable != null)
            {
                LoadLeagueTeams(leagueTable.SelectNodes("tbody/tr[@data-team-slug]"));
                PopulateLeagueDivisions();
            }      
        }

        private void PopulateLeagueDivisions()
        {
            var leagueDivisionGuesser = new LeagueDivisionGuesser();
            leagueDivisionGuesser.Init(this);
            leagueDivisionGuesser.GuessDivisionsFromDividers();
        }

        private void LoadLeagueTeams(HtmlNodeCollection teamRows)
        {
            foreach (HtmlNode team in teamRows)
            {
                var t = new Team();
                t.Init(team);
                Teams.Add(t);
            }
        }

        private HtmlNode LoadLeagueTable()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://www.bbc.com/sport/football/" + LeagueId + "/table");
            return doc.DocumentNode.SelectSingleNode("//table[@data-competition-slug='" + LeagueId + "']");
        }

        public bool IsTopLeague(String league)
        {
            if (league.Contains("premier")) { return true; }
            return false;
        }
    }
}
