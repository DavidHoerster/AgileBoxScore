using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AgileBoxScore.Runner;
using Newtonsoft.Json;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hit enter to begin...");
            Console.ReadLine();

            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var games = new Dictionary<String, Game>{
                { "pitstl", new Game("pitstl", 0, true, "PIT", "STL", 0, 0, 0, 0, 0, 0, 0)},
                { "wshphl", new Game("wshphl", 0, true, "WSH", "PHL", 0, 0, 0, 0, 0, 0, 0)},
                { "texbal", new Game("texbal", 0, true, "TEX", "BAL", 0, 0, 0, 0, 0, 0, 0)}
            };
            var fileContents = File.ReadAllText(String.Format(@"{0}\gameinfo.json", ConfigurationManager.AppSettings["filePath"]));
            IEnumerable <GameEvent> gameEvents = JsonConvert.DeserializeObject<IEnumerable<GameEvent>>(fileContents);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:33805/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));


                foreach (var gameEvent in gameEvents)
                {

                    var game = games[gameEvent.Id];
                    if (gameEvent.IsTop)
                    {
                        //away team
                        game.AwayError += gameEvent.Error;
                        game.AwayHits += gameEvent.Hit;
                        game.AwayRuns += gameEvent.Run;
                    }
                    else
                    {
                        //home team
                        game.HomeError += gameEvent.Error;
                        game.HomeHits += gameEvent.Hit;
                        game.HomeRuns += gameEvent.Run;
                    }
                    game.Inning = gameEvent.Inning;
                    game.Outs = gameEvent.Out;
                    game.IsTopOfInning = gameEvent.IsTop;

                    var response = await client.PutAsJsonAsync("api/game/" + game.Id, 
                        game);
                    await Task.Delay(750);
                }
            }
        }

    }
}
