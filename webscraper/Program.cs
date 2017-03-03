using System;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //League PremierLeague = new League("premier-league");
            //PrintInfo p = new PrintInfo("Manchester United", PremierLeague);
            //League PremierLeague = new League("championship");
            //PrintInfo p = new PrintInfo("Nottingham Forest", PremierLeague);
            League PremierLeague = new League("scottish-premiership");
            PrintInfo p = new PrintInfo("Rangers", PremierLeague);
            p.PrintLeagueSpots();
            p.PrintLeagueStatusFor();
            p.PrintLastTenProgressFor();
            p.PrintLastGamesInfo();

            Console.ReadKey();
        }
    }
}
