using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using webscraper;
namespace WPFLeaguePosition
{    
    public partial class MainWindow : Window
    {
        League league { get; set; }
        TextBoxWriter _writer = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void cboLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var choiceItem = comboBox.SelectedItem as ComboBoxItem;
            BeforeLoadLeague();
            
            league = new League();
            league.LeagueName = choiceItem.Content.ToString();
            TeamInfo.Text = league.LeagueName + " " + await LoadLeague(choiceItem.Tag.ToString());

            AfterLoadLeague();
        }

        private void BeforeLoadLeague()
        {
            BusyIndicator.Visibility = Visibility.Visible;
            cboTeam.IsEnabled = false;
            TeamInfo.Clear();
        }
        private void AfterLoadLeague()
        {
            FillCboTeam(league);
            cboTeam.IsEnabled = true;
            BusyIndicator.Visibility = Visibility.Hidden;
        }

        private async Task<string> LoadLeague(string leagueId)
        {
            try
            {
                await Task.Run(() => {
                    league.Init(leagueId);
                    return "working";
                    });
                return " loaded please select a Team";
            }
            catch (Exception)
            {
                return "failed to get League";
            }

        }

        private void cboTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            var comboBox = sender as ComboBox;
            var choiceItem = comboBox.SelectedItem as string;
            if (choiceItem != null)
            {
                TeamInfo.Clear();
                PrintTeamInfoTo(choiceItem, TeamInfo);
            }
        }

        private void PrintTeamInfoTo(string choiceItem, TextBox teamInfo)
        {
            PrintInfo p = new PrintInfo(choiceItem, league);
            _writer = new TextBoxWriter(TeamInfo);
            Console.SetOut(_writer);
            p.PrintSearchTeamLeaguePosition();
            p.PrintLeagueSpots();
            p.PrintLeagueStatusFor();
            p.PrintLastTenProgressFor();
            p.PrintLastGamesInfo();
        }

        private void FillCboTeam(League league)
        {
            cboTeam.ItemsSource = ConvertTeamsToListItems(league);
            cboTeam.IsEnabled = true;
            cboTeam.SelectedIndex = 0;
        }

        private IEnumerable ConvertTeamsToListItems(League league)
        {
            return league.Teams.Select(t => t.Name).ToArray();
        }
                
    }
}
