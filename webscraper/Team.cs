using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace webscraper
{
    public class Team
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDiff { get; set; }
        public int Points { get; set; }
        public List<string> LastTen;
        public int LastTenPoints { get; set; }
        public bool LeagueBubbleDivider { get; set; }
        public string LeagueSection { get; set; }
        public Team()
        {
            LastTen = new List<string>();
            LastTenPoints = 0;
        }
        public void Init(HtmlNode xmlTeam) 
        {
            Name = xmlTeam.SelectSingleNode("td[@class = 'team-name']/a").InnerText;
            Position = int.Parse(xmlTeam.SelectSingleNode("td[@class = 'position']/span[@class = 'position-number']").InnerText);
            Played = int.Parse(xmlTeam.SelectSingleNode("td[@class='played']").InnerText);
            Won = int.Parse(xmlTeam.SelectSingleNode("td[@class='won']").InnerText);
            Drawn = int.Parse(xmlTeam.SelectSingleNode("td[@class='drawn']").InnerText);
            Lost = int.Parse(xmlTeam.SelectSingleNode("td[@class='lost']").InnerText);
            GoalsFor = int.Parse(xmlTeam.SelectSingleNode("td[@class='for']").InnerText);
            GoalsAgainst = int.Parse(xmlTeam.SelectSingleNode("td[@class='against']").InnerText);
            GoalDiff = int.Parse(xmlTeam.SelectSingleNode("td[@class='goal-difference']").InnerText);
            Points = int.Parse(xmlTeam.SelectSingleNode("td[@class='points']").InnerText);
            getLastTenInfo(xmlTeam.SelectNodes("td[@class='last-10-games']/ol/li"));
            LeagueBubbleDivider = GetDivider(xmlTeam.GetAttributeValue("class", "no team class"));
        }

        private bool GetDivider(string divider)
        {
            return (divider.Contains("divider"));
        }

        private void getLastTenInfo(HtmlNodeCollection games)
        {
            foreach (HtmlNode game in games)
            {
                LastTen.Add(game.GetAttributeValue("title", "no game found"));
                LastTenPoints += getGamePoint(game.GetAttributeValue("class", "loss"));
            }
        }
        private int getGamePoint(string result)
        {
            if (result == "draw") { return 1; }
            if (result == "win") { return 3; }
            return 0;
        }
    }
}
