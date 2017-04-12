using NUnit.Framework;

namespace webscraper.test
{
    [TestFixture]
    public class LeagueTest
    {
        private League league;
        
        [SetUp]
        public void Setup()
        {
            league = new League();
        }
        [Test]
        public void IsPremierLeagueTopLeague()
        {
            Assert.AreEqual(true, league.IsTopLeague("premier-league"));
        }
        [Test]
        public void IsChampionshipLeagueTopLeague()
        {
            Assert.AreEqual(false, league.IsTopLeague("championship"));
        }
        [Test]
        public void IsScottishPremierLeagueTopLeague()
        {
            Assert.AreEqual(true, league.IsTopLeague("scottish-premiership"));
        }
        //[TestMethod]
        //public void IsLaLigaTopLeague()
        //{
        //    Assert.AreEqual(true, _league.IsTopLeague("spanish-la-liga"));
        //}
        [Test]
        public void PremierLeagueInitSuccess()
        {
            league.Init("premier-league");
            Assert.AreEqual(20, league.Teams.Count);
        }
        [Test]
        public void NoLeagueInitReturnZeroTeams()
        {
            league.Init("premier");
            Assert.AreEqual(0, league.Teams.Count);
        }
        [Test]
        public void ChampionshipLeagueInitSuccess()
        {
            league.Init("championship");
            Assert.AreEqual(24, league.Teams.Count);
        }
        [Test]
        public void ScottishPremierLeagueInitSuccess()
        {
            league.Init("scottish-premiership");
            Assert.AreEqual(12, league.Teams.Count);
        }
        [Test]
        public void ScottishLeagueOneInitSuccess()
        {
            league.Init("scottish-league-one");
            Assert.AreEqual(10, league.Teams.Count);
        }
        [Test]
        public void NationalLeagueInitSuccess()
        {
            league.Init("national-league");
            Assert.AreEqual(24, league.Teams.Count);
        }
    }
}
