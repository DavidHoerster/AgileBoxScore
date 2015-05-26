using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileBoxScore.Runner
{
    public class Game
    {
        public Game() { }
        public Game(String id, Int32 inning, Boolean isTop, String home, String away,
            Int32 homeScore, Int32 awayScore, Int32 homeHits, Int32 awayHits,
            Int32 homeError, Int32 awayError, Int32 outs)
        {
            Id = id;
            Inning = inning;
            IsTopOfInning = isTop;
            HomeTeam = home;
            AwayTeam = away;
            HomeRuns = homeScore;
            AwayRuns = awayScore;
            HomeHits = HomeHits;
            AwayHits = awayHits;
            HomeError = homeError;
            AwayError = awayError;
            Outs = outs;
        }

        public String Id { get; set; }
        public Int32 Inning { get; set; }
        public Boolean IsTopOfInning { get; set; }
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }
        public Int32 HomeRuns { get; set; }
        public Int32 AwayRuns { get; set; }
        public Int32 HomeHits { get; set; }
        public Int32 AwayHits { get; set; }
        public Int32 HomeError { get; set; }
        public Int32 AwayError { get; set; }
        public Int32 Outs { get; set; }
    }
}