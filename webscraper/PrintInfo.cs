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

        public PrintInfo(string teamName, LeagueTeams League)
        {
            DividerTeams = new List<PrintDividerTeam>();
            SearchTeam = League.Teams.Find(t => t.Name == teamName);
            foreach (LeagueDivisions division in League.Divisions)
            {
                var printDividerTeam = new PrintDividerTeam();
                printDividerTeam.DividerTeam = League.Teams.Find(t => t.Position == division.StartPosition);
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
            //Console.WriteLine(String.Format($"League leaders {Leaders.Name} are at: {Leaders.Points} points"));
            //Console.WriteLine(String.Format($"Final Champions League place is {LastGoodSpot.Name} at: {LastGoodSpot.Points} points"));
            //Console.WriteLine(String.Format($"{DropZone.Name} are top of the drop at: {DropZone.Points} points"));
        }
        public void PrintLeagueStatusFor()
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
            //Console.WriteLine(String.Format($"{SearchTeam.Name} are {(Leaders.Points - SearchTeam.Points)} points behind the leaders with {(Leaders.Played - SearchTeam.Played)} game(s) in hand"));
            //Console.WriteLine(String.Format($"{SearchTeam.Name} are {(LastGoodSpot.Points - SearchTeam.Points)} points back of the champions league with {(LastGoodSpot.Played - SearchTeam.Played)} game(s) in hand"));
            //Console.WriteLine(String.Format($"{SearchTeam.Name} are {(SearchTeam.Points - DropZone.Points)} points above the drop with {(DropZone.Played - SearchTeam.Played)} game(s) in hand"));
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
    }
}
