using System;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            //largelyDead with the wpf ui
            League league = new League();
            league.Init("premierleague");
            PrintInfo p = new PrintInfo("Manchester United", league);
            //League PremierLeague = new League("championship");
            //PrintInfo p = new PrintInfo("Nottingham Forest", PremierLeague);
            //League PremierLeague = new League("scottish-premiership");
            //PrintInfo p = new PrintInfo("Rangers", PremierLeague);
            p.PrintLeagueSpots();
            p.PrintLeagueStatusFor();
            p.PrintLastTenProgressFor();
            p.PrintLastGamesInfo();

            Console.ReadKey();
        }
    }
}
