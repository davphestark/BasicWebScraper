using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace webscraper
{
    public class LeagueTeams
    {
        public string LeagueName { get; set; }
        public List<Team> Teams;
        public List<LeagueDivisions> Divisions;
        public bool IsTopOfPyramid;
        public LeagueTeams()
        {
            Teams = new List<Team>();
            Divisions = new List<LeagueDivisions>();
        }
        public LeagueTeams(string league) : this()
        {
            LeagueName = league;
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
            //guess sections by positions and dividers
            //get all dividers
            var dividerPosition = Teams.FindAll(t => t.LeagueBubbleDivider).Select(t => t.Position).ToArray();
            Divisions.Add(new LeagueDivisions { StartPosition = 1, EndPostion = 1, DividerName = "League Leader", DivisionName = "League Leader" });
            int topBottomsplitIndex = 0;
            for (int i = 0; i < dividerPosition.Length; i++)
            {
                if (dividerPosition[i] < 10) { dividerPosition[i] = dividerPosition[i] - 1; }
                if (dividerPosition[i] > 9 && topBottomsplitIndex == 0) { topBottomsplitIndex = i; }
            }
            int[] divisionsTopSplit = dividerPosition.Take(topBottomsplitIndex).ToArray();
            int[] divisionsBottomSplit = dividerPosition.Skip(topBottomsplitIndex).ToArray();
            for (int i = 0; i < divisionsTopSplit.Length; i++)
            {
                var division = new LeagueDivisions();
                division.StartPosition = divisionsTopSplit[i];
                if (IsTopOfPyramid) { division.DividerName = GuessTopDividerByPositionTopPyramidLeague(divisionsTopSplit[i], divisionsTopSplit); }
                else {division.DividerName = GuessTopDividerByPositionRegularLeague(divisionsTopSplit[i], divisionsTopSplit); }
                Divisions.Add(division);
            }
            for (int i = 0; i < divisionsBottomSplit.Length; i++)
            {
                var division = new LeagueDivisions();
                division.StartPosition = divisionsBottomSplit[i];
                division.DividerName = GuessBottomDividersByPosition(divisionsBottomSplit[i], divisionsBottomSplit); 
                Divisions.Add(division);
            }

            //set the value on team object
        }
        private string GuessTopDividerByPositionTopPyramidLeague(int position, int[] numberInGroup)
        {
           
            if (position == numberInGroup[0]) { return "Last Champions League Spot"; }
            if (position == numberInGroup[1]) { return "Last Europa Spot"; }
           
            if (numberInGroup.Length == 3)
            {                
                if (position == numberInGroup[2]) { return "Last Playoff Spot"; }
            }
            return "none";
        }
        private string GuessTopDividerByPositionRegularLeague(int position, int[] numberInGroup)
        {
            if (position == 2 || position == 3) { return "Automatic Promotion"; }
            if (position > 1 && is3or4TeamsInGroup(numberInGroup[0], numberInGroup[1])) { return "Last Promotion Playoff Spot"; }
            return "none";
        }

        private bool is3or4TeamsInGroup(int startGroupPosition, int endGroupPosition)
        {
            return endGroupPosition - startGroupPosition == 3 || endGroupPosition - startGroupPosition == 4;
        }

        private string GuessBottomDividersByPosition(int position, int[] numberInGroup)
        {
            if (position > 10 && numberInGroup.Length == 1) { return "Top of Relegation Zone"; }
            if (numberInGroup.Length == 2)
            {
                if (position == numberInGroup[0]) { return "Top of Relegation Zone Playoff"; }
                if (position == numberInGroup[1]) { return "Top of Relegation Zone"; }
            }
            return "none";
        }

    }

}
