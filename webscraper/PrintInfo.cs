using System;
using System.Collections.Generic;

namespace webscraper
{
    public class PrintInfo
    {
        public Team Leaders { get; set; }
        public List<PrintDividerTeam> DividerTeams {get; set;}
        public Team LastGoodSpot { get; set; }
        public Team DropZone { get; set; }
        public Team SearchTeam { get; set; }
		private string LeagueName { get; set; }

        public PrintInfo(string teamName, League league)
        {
            DividerTeams = new List<PrintDividerTeam>();
            SearchTeam = league.Teams.Find(t => t.Name == teamName);
            LeagueName = league.LeagueName;
            foreach (LeagueDivisions division in league.Divisions)
            {
                var printDividerTeam = new PrintDividerTeam();
                printDividerTeam.DividerTeam = league.Teams.Find(t => t.Position == division.StartPosition);
                printDividerTeam.DivisionName = division.DividerName;
                DividerTeams.Add(printDividerTeam);
            }            
        }

        public void PrintLeagueSpots()
        {
            foreach (PrintDividerTeam t in DividerTeams)
            {
                Console.WriteLine(String.Format($"The {t.DivisionName} is {t.DividerTeam.Name} at: {t.DividerTeam.Points} points"));
            }
        }
        public void PrintLeagueStatusFor()
        {
            if (SearchTeam != null)
            {               
                foreach (PrintDividerTeam t in DividerTeams)
                {
                    Console.WriteLine(String.Format("{0} are {1} points {2} the {3} with {4} game(s) in hand",
                        SearchTeam.Name,
                        Math.Abs(t.DividerTeam.Points - SearchTeam.Points),
                        (t.DividerTeam.Points - SearchTeam.Points) < 0 ? "ahead" : "behind",
                        t.DivisionName,
                        (t.DividerTeam.Played - SearchTeam.Played)));
                }
            }
        }

        private string ConvertPositionToEnglish(int position)
        {
            if (position > 3) { return position + "th"; };
            if (position == 3) { return position + "rd"; };
            if (position == 2) { return position + "nd"; };
            if (position == 1) { return position + "st"; };
            return "";
        }

        public void PrintLastTenProgressFor()
        {
            foreach (PrintDividerTeam t in DividerTeams)
            {
                Console.WriteLine(String.Format("{0} {1} {2} points on {3} in the last ten games", SearchTeam.Name, (SearchTeam.LastTenPoints - t.DividerTeam.LastTenPoints) < 0 ? "loss" : "gained", (SearchTeam.LastTenPoints - t.DividerTeam.LastTenPoints), t.DividerTeam.Name));
            }
        }
        public void PrintLastGamesInfo()
        {
            Console.WriteLine(String.Format($"the last ten league games gained {SearchTeam.LastTenPoints} for {SearchTeam.Name} are:"));
            foreach (string game in SearchTeam.LastTen)
            {
                Console.WriteLine(game);
            }
        }
        public void PrintSearchTeamLeaguePosition()
        {
            if (SearchTeam != null)
            {
                Console.WriteLine(String.Format($"{SearchTeam.Name} is currently in {ConvertPositionToEnglish(SearchTeam.Position)} place in the {LeagueName}"));
            }
        }
    }
}
