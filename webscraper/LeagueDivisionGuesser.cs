using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webscraper
{
    public class LeagueDivisionGuesser
    {
        private League _league;

        public void Init(League league)
        {
            _league = league;
        }
        public void GuessDivisionsFromDividers()
        {
            AddLeagueLeadersToDivisions();
            int[] dividerArray = BuildDividerArray();

            int[][] divisionsSplit =  AdjustTopAndSplitTopAndBottomArrays(dividerArray);

            for (int i = 0; i < divisionsSplit.Length; i++)
            {
                GuessAndFillDivisions(divisionsSplit[i]);
            }
        }

        private void GuessAndFillDivisions(int[] splitDivisions)
        {
            for (int i = 0; i < splitDivisions.Length; i++)
            {
                var division = new LeagueDivisions();
                division.StartPosition = splitDivisions[i];
                division.DividerName = SetDivisionNameBySplit(splitDivisions[i], splitDivisions);
                
                _league.Divisions.Add(division);
            }
        }

        private string SetDivisionNameBySplit(int position, int[] splitDivisions)
        {
            if (position < 10) {return SetTopDivisionsName(position, splitDivisions);}
            else { return GuessBottomDividersByPosition(position, splitDivisions); }            
        }

        private string SetTopDivisionsName(int position, int[] splitDivisions)
        {
            if (_league.IsTopOfPyramid)
            {
                return GuessTopDividerByPositionTopPyramidLeague(position, splitDivisions);
            }
            return GuessTopDividerByPositionRegularLeague(position, splitDivisions);
        }
        private int[][] AdjustTopAndSplitTopAndBottomArrays(int[] dividerArray)
        {
            int topBottomsplitIndex = 0;
            for (int i = 0; i < dividerArray.Length; i++)
            {
                if (dividerArray[i] < 10) { dividerArray[i] = dividerArray[i] - 1; }
                if (dividerArray[i] > 9 && topBottomsplitIndex == 0) { topBottomsplitIndex = i; }
            }
            int[][] ret = new int[2][];
            ret[0] = dividerArray.Take(topBottomsplitIndex).ToArray();
            ret[1] = dividerArray.Skip(topBottomsplitIndex).ToArray();
            return ret;
        }

        private int[] BuildDividerArray()
        {
            return _league.Teams.FindAll(t => t.LeagueBubbleDivider).Select(t => t.Position).ToArray();
        }

        private void AddLeagueLeadersToDivisions()
        {
            _league.Divisions.Add(new LeagueDivisions { StartPosition = 1, EndPostion = 1, DividerName = "League Leader", DivisionName = "League Leader" });
        }

        private string GuessTopDividerByPositionTopPyramidLeague(int position, int[] numberInGroup)
        {

            if (position == numberInGroup[0]) { return "Last Champions League Spot"; }
            if (position == numberInGroup[1]) { return "Last Europa Spot"; }

            if (numberInGroup.Length == 3)
            {
                if (position == numberInGroup[2]) { return "Last Playoff Spot"; }
            }
            return "none";
        }
        private string GuessTopDividerByPositionRegularLeague(int position, int[] numberInGroup)
        {
            if (position == 2 || position == 3) { return "Automatic Promotion"; }
            if (position > 1 && is3or4TeamsInGroup(numberInGroup[0], numberInGroup[1])) { return "Last Promotion Playoff Spot"; }
            return "none";
        }

        private bool is3or4TeamsInGroup(int startGroupPosition, int endGroupPosition)
        {
            return endGroupPosition - startGroupPosition == 3 || endGroupPosition - startGroupPosition == 4;
        }

        private string GuessBottomDividersByPosition(int position, int[] numberInGroup)
        {
            if (position > 10 && numberInGroup.Length == 1) { return "Top of Relegation Zone"; }
            if (numberInGroup.Length == 2)
            {
                if (position == numberInGroup[0]) { return "Top of Relegation Zone Playoff"; }
                if (position == numberInGroup[1]) { return "Top of Relegation Zone"; }
            }
            return "none";
        }
    }
}
