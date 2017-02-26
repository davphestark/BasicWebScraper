using System;

namespace webscraper
{
    public class PrintInfo
    {
        public Team Leaders { get; set; }
        public Team LastGoodSpot { get; set; }
        public Team DropZone { get; set; }
        public Team SearchTeam { get; set; }

        public PrintInfo(string teamName, LeagueTeams League)
        {
            //this needs rework so the postions aren't hard coded
            Leaders = League.Teams.Find(t => t.Position == 1);
            LastGoodSpot = League.Teams.Find(t => t.Position == 4);
            DropZone = League.Teams.Find(t => t.Position == 18);
            SearchTeam = League.Teams.Find(t => t.Name == teamName);
        }

        public void PrintLeagueSpots()
        {
            Console.WriteLine(String.Format($"League leaders {Leaders.Name} are at: {Leaders.Points} points"));
            Console.WriteLine(String.Format($"Final Champions League place is {LastGoodSpot.Name} at: {LastGoodSpot.Points} points"));
            Console.WriteLine(String.Format($"{DropZone.Name} are top of the drop at: {DropZone.Points} points"));
        }
        public void PrintLeagueStatusFor()
        {
            Console.WriteLine(String.Format($"{SearchTeam.Name} are {(Leaders.Points - SearchTeam.Points)} points behind the leaders with {(Leaders.Played - SearchTeam.Played)} game(s) in hand"));
            Console.WriteLine(String.Format($"{SearchTeam.Name} are {(LastGoodSpot.Points - SearchTeam.Points)} points back of the champions league with {(LastGoodSpot.Played - SearchTeam.Played)} game(s) in hand"));
            Console.WriteLine(String.Format($"{SearchTeam.Name} are {(SearchTeam.Points - DropZone.Points)} points above the drop with {(DropZone.Played - SearchTeam.Played)} game(s) in hand"));
        }
        public void PrintLastTenProgressFor()
        {
            Console.WriteLine(String.Format("{0} {1} {2} points on {3} in the last ten games", SearchTeam.Name, (SearchTeam.LastTenPoints - Leaders.LastTenPoints) < 0 ? "loss" : "gained", (SearchTeam.LastTenPoints - Leaders.LastTenPoints), Leaders.Name));
            Console.WriteLine(String.Format("{0} {1} {2} points on {3} in the last ten games", SearchTeam.Name, (SearchTeam.LastTenPoints - LastGoodSpot.LastTenPoints) < 0 ? "loss" : "gained", (SearchTeam.LastTenPoints - LastGoodSpot.LastTenPoints), LastGoodSpot.Name));
            Console.WriteLine(String.Format("{0} {1} {2} points on {3} in the last ten games", SearchTeam.Name, (SearchTeam.LastTenPoints - DropZone.LastTenPoints) < 0 ? "loss" : "gained", (SearchTeam.LastTenPoints - DropZone.LastTenPoints), DropZone.Name));
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
