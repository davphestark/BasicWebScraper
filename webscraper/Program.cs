using System;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            LeagueTeams PremierLeague = new LeagueTeams("premier-league");
            PrintInfo p = new PrintInfo("Manchester United", PremierLeague);
            p.PrintLeagueSpots();
            p.PrintLeagueStatusFor();
            p.PrintLastTenProgressFor();
            p.PrintLastGamesInfo();

            Console.ReadKey();
        }
    }
}
