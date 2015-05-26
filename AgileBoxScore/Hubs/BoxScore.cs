using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using AgileBoxScore.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AgileBoxScore.Hubs
{
    public class BoxScore
    {
        // Singleton instance
        private readonly static Lazy<BoxScore> _instance = new Lazy<BoxScore>(() => new BoxScore(GlobalHost.ConnectionManager.GetHubContext<GameHub>().Clients));

        private readonly ConcurrentDictionary<string, Game> _games = new ConcurrentDictionary<string, Game>();
        private readonly object _updateGamesLock = new object();

        private volatile bool _updatingGames = false;


        private BoxScore(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _games.Clear();
            var games = new List<Game>
            {
                new Game("pitstl", 0, true, "PIT", "STL", 0, 0, 0, 0, 0, 0, 0),
                new Game("wshphl", 0, true, "WSH", "PHL", 0, 0, 0, 0, 0, 0, 0),
                new Game("texbal", 0, true, "TEX", "BAL", 0, 0, 0, 0, 0, 0, 0)
            };
            games.ForEach(game => _games.TryAdd(game.Id, game));
        }

        public static BoxScore Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public IEnumerable<Game> GetAllGames()
        {
            return _games.Values;
        }

        internal void UpdateGame(String id, Game game)
        {

            if (TryUpdateGameScore(id, game))
            {
                BroadcastGameScore(id, game);
            }

        }

        private bool TryUpdateGameScore(String id, Game game)
        {
            var theGame = _games[id];
            theGame.AwayError = game.AwayError;
            theGame.AwayHits = game.AwayHits;
            theGame.AwayRuns = game.AwayRuns;
            theGame.AwayTeam = game.AwayTeam;
            theGame.HomeError = game.HomeError;
            theGame.HomeHits = game.HomeHits;
            theGame.HomeRuns = game.HomeRuns;
            theGame.HomeTeam = game.HomeTeam;
            theGame.Inning = game.Inning;
            theGame.IsTopOfInning = game.IsTopOfInning;
            theGame.Outs = game.Outs;

            return true;
        }
        private void BroadcastGameScore(String id, Game game)
        {
            var theGame = _games[id];
            Clients.All.updateGameScore(theGame);
        }
    }
}