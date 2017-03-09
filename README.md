# BasicWebScraper

Basic web scraper for the BBC's football tables.
has a dependancy with HtmlAgilityPack

Idea is to select a league and a team within that league and it will write out to the console who is leading the league and who is at the cutoff of all the important divisions in the league table with the relation of the selected team to all of the teams occupying the division cuts places.

example: running it for the Premier league and Manchester United on 3/3/2017 results in:

The League Leader is Chelsea at: 63 points

The Last Champions League Spot is Arsenal at: 50 points

The Last Europa Spot is Liverpool at: 49 points

The Top of Relegation Zone is Crystal Palace at: 22 points

Manchester United are 15 points behind the League Leader with 1 game(s) in hand

Manchester United are 2 points behind the Last Champions League Spot with 0 game(s) in hand

Manchester United are 1 points behind the Last Europa Spot with 1 game(s) in hand

Manchester United are 26 points ahead the Top of Relegation Zone with 1 game(s) in hand

Manchester United gained 1 points on Chelsea in the last ten games

Manchester United gained 8 points on Arsenal in the last ten games

Manchester United gained 9 points on Liverpool in the last ten games

Manchester United gained 17 points on Crystal Palace in the last ten games

the last ten league games gained 24 for Manchester United are:

Win v Crystal Palace 1 - 2 on 14th December 2016

Win v West Bromwich Albion 0 - 2 on 17th December 2016

Win v Sunderland 3 - 1 on 26th December 2016

Win v Middlesbrough 2 - 1 on 31st December 2016

Win v West Ham United 0 - 2 on 2nd January 2017

Draw v Liverpool 1 - 1 on 15th January 2017

Draw v Stoke City 1 - 1 on 21st January 2017

Draw v Hull City 0 - 0 on 1st February 2017

Win v Leicester City 0 - 3 on 5th February 2017

Win v Watford 2 - 0 on 11th February 2017

---
Update 3/8/2017
Added a UI to swap out leagues and teams easier

WPFLeaguePosition.sln has the UI 
webscraper.sln still runs the old console version, but is otherwise dead.
