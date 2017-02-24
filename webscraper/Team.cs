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
        public Team()
        {
            LastTen = new List<string>();
        }
        public Team(HtmlNode xmlTeam) : this()
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
            HtmlNodeCollection games = xmlTeam.SelectNodes("td[@class='last-10-games']/ol/li");
            foreach (HtmlNode game in games)
            {
                LastTen.Add(game.GetAttributeValue("title", "no game found"));
            }
        }
    }
}
