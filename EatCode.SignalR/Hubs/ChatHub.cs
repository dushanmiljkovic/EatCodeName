using Microsoft.AspNetCore.SignalR;
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

            if (status)
            {
                // Send msg to chat
                await Clients.All.SendAsync("ReceiveMessage", user, message, status);

                // Update scoreboard
                var scoreboard = GetScoreboar();

                var toShow = scoreboard.Select(x => new ReciteVote()
                {
                    Name = x.Key,
                    Score = x.Value
                }).ToList();

                await Clients.All.SendAsync("ReceiveScoreboar", toShow);
            }

        }

        public async Task SendStoreMessage(string user)
        {
            var status = StoreVores();
            var message = "Uspesno Storovani useri hvala lepo molim lepo";
            await Clients.All.SendAsync("StoreMessage", user, message, status);
        }

        private bool ParseMessage(string message)
        {
            var parse = message.Split('/');
            if (parse.First().Trim() == "vote" && parse.Length == 3)
            {
                var recipe = parse[1].Trim();
                var score = parse[2].Trim();

                var votedResult = AddVote(recipe, score);
            }

            if (parse.First().Trim() == "deleteVote" && parse.Length == 2)
            {
                var recipe = parse[1].Trim();
                var votedResult = RemoveVote(recipe);
            }

            return true;
        }

        private bool AddVote(string recipe, string score)
        {
            var redisDb = new ScoreboardStack();

            double dScore;
            try
            {
                dScore = Convert.ToDouble(score);
            }
            catch
            {
                dScore = 0;
            }
            var status = redisDb.AddItemToScoreboard(recipe, dScore);
            return status;
        }

        private bool RemoveVote(string recipe)
        {
            var redisDb = new ScoreboardStack();
            var status = redisDb.DeleteItemFromScoreboard(recipe);
            return status;
        }

        private IDictionary<string, double> GetScoreboar()
        {
            var redisDb = new ScoreboardStack();
            var status = redisDb.GetScoreboard();
            return status;
        }

        private bool StoreVores()
        {
            var redisDb = new ScoreboardStack();
            var status = redisDb.GetScoreboard();

            var sotored = true; // store data into MongoDB

            if (sotored)
            {
                var status2 = redisDb.DeleteScoreboard();
            }

            return sotored;
        }
    }

    public class ReciteVote
    {
        public string Name { get; set; }
        public double Score { get; set; }

    }
}
