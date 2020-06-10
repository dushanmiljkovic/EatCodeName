using Microsoft.AspNetCore.SignalR;
using Models.Domein;
using Redis.Stack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Hubs
{
    public class ScoreboardHub : Hub
    {
        public async Task ShowScoreboar()
        {
          
            await Clients.All.SendAsync("ReceiveScoreboar", GetScoreboardAsList());
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveScoreboar", GetScoreboardAsList());
        }

        private IList<RecipeVote> GetScoreboardAsList()
        {
            var redisDb = new ScoreboardStack();
            var scoreboard = redisDb.GetScoreboard();
            return scoreboard.Select(x => new RecipeVote()
            {
                Name = x.Key,
                Score = x.Value
            }).OrderByDescending(o => o.Score).ToList(); 
        }

    }
}