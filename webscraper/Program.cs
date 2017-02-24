using System;

namespace webscraper
{
    class Program
    {
        static void Main(string[] args)
        {
            LeagueTeams PremierLeague = new LeagueTeams("premier-league");
            PrintOut("Manchester United", PremierLeague);

            Console.ReadKey();
        }
        private static void PrintOut(string teamName, LeagueTeams League)
        {
            Team leaders = League.Teams.Find(t => t.Position == 1);
            Team lastChamps = League.Teams.Find(t => t.Position == 4);
            Team dropZone = League.Teams.Find(t => t.Position == 18);
            Team searchedTeam = League.Teams.Find(t => t.Name == teamName);
            Console.WriteLine(String.Format($"League leaders {leaders.Name} are at: {leaders.Points} points"));
            Console.WriteLine(String.Format($"Final Champions League place is {lastChamps.Name} at: {lastChamps.Points} points"));
            Console.WriteLine(String.Format($"{dropZone.Name} are top of the drop at: {dropZone.Points} points"));
            Console.WriteLine(String.Format($"{searchedTeam.Name} are {(leaders.Points - searchedTeam.Points)} points behind the leaders"));
            Console.WriteLine(String.Format($"{searchedTeam.Name} are {(lastChamps.Points - searchedTeam.Points)} points back of the champions league"));
            Console.WriteLine(String.Format($"{searchedTeam.Name} are {(searchedTeam.Points - dropZone.Points)} points above the drop"));
            Console.WriteLine();
            Console.WriteLine(String.Format($"the last ten league games for { searchedTeam.Name} are:" ));
            foreach (string game in searchedTeam.LastTen)
            {
                Console.WriteLine(game);
            }
        }

    }
}
