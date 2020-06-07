using Microsoft.AspNetCore.SignalR;
using Models.Domein;
using Redis.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var status = ParseMessage(message);

            if (status.Item1)
            {
                // Send msg to chat
                await Clients.All.SendAsync("ReceiveMessage", user, message, status);

                // Update scoreboard 
                var toShow = CastScoreToList(GetScoreboar());

                await Clients.All.SendAsync("ReceiveScoreboar", toShow);
            }

        }

        public async Task SendStoreMessage(string user)
        {
            var status = StoreVores();
            var message = "Uspesno Storovani useri hvala lepo molim lepo";
            await Clients.All.SendAsync("StoreMessage", user, message, status);
        }

        private (bool, string) ParseMessage(string message)
        {
            var parse = message.Split('/');

            // Vote 
            if (parse.First().Trim() == "vote" && parse.Length == 3)
            {
                var recipe = parse[1].Trim();
                var score = parse[2].Trim();

                var votedResult = AddVote(recipe, score);
            }
            // Delete Vote
            else if (parse.First().Trim() == "deleteVote" && parse.Length == 2)
            {
                var recipe = parse[1].Trim();
                var votedResult = RemoveVote(recipe);
            }

            return (true, message);
        }

        private bool AddVote(string recipe, string score)
        {
            var redisDb = new ScoreboardStack();
            return redisDb.AddItemToScoreboard(recipe, CastStringToDouble(score));
        }

        private bool RemoveVote(string recipe)
        {
            var redisDb = new ScoreboardStack();
            return redisDb.DeleteItemFromScoreboard(recipe);
        }

        private IDictionary<string, double> GetScoreboar()
        {
            var redisDb = new ScoreboardStack();
            return redisDb.GetScoreboard();
        }

        private bool StoreVores()
        {
            var redisDb = new ScoreboardStack();

            var scoreboard = CastScoreToList(redisDb.GetScoreboard());

            var sotored = true; // store data into MongoDB

            if (sotored)
            {
                var status2 = redisDb.DeleteScoreboard();
                return status2;
            }

            return sotored;
        }


        private double CastStringToDouble(string score)
        {
            try
            {
                return Convert.ToDouble(score);
            }
            catch
            {
                return 0;
            }
        }

        private List<RecipeVote> CastScoreToList(IDictionary<string, double> score)
            => score.Select(x => new RecipeVote()
            {
                Name = x.Key,
                Score = x.Value
            }).ToList();
    }
}
