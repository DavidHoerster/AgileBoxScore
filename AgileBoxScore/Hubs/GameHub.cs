using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgileBoxScore.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AgileBoxScore.Hubs
{
    [HubName("gameBox")]
    public class GameHub : Hub
    {
        private readonly BoxScore _boxScore;

        public GameHub() : this(BoxScore.Instance) { }

        public GameHub(BoxScore boxScore)
        {
            _boxScore = boxScore;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _boxScore.GetAllGames();
        }

        public void UpdateGame(String id, Game game)
        {
            _boxScore.UpdateGame(id, game);
        }
    }
}