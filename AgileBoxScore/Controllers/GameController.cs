using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgileBoxScore.Hubs;
using AgileBoxScore.Models;

namespace AgileBoxScore.Controllers
{
    public class GameController : ApiController
    {

        private GameHub _hub;

        public GameController() { _hub = new GameHub(); }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        public IHttpActionResult Put(String id, Game game)
        {
            _hub.UpdateGame(id, game);

            return Ok();
        }
    }
}
