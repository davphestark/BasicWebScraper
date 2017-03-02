using System;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //LeagueTeams PremierLeague = new LeagueTeams("premier-league");
            //PrintInfo p = new PrintInfo("Manchester United", PremierLeague);
            LeagueTeams PremierLeague = new LeagueTeams("championship");
            PrintInfo p = new PrintInfo("Nottingham Forest", PremierLeague);
            p.PrintLeagueSpots();
            p.PrintLeagueStatusFor();
            p.PrintLastTenProgressFor();
            p.PrintLastGamesInfo();

            Console.ReadKey();
        }
    }
}
