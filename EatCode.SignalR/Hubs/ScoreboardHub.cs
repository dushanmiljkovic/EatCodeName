﻿using Microsoft.AspNetCore.SignalR;
using Models.Domein;
using Redis.Stack;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Hubs
{
    public class ScoreboardHub : Hub
    {
        public async Task ShowScoreboar()
        {
            var redisDb = new ScoreboardStack();
            var scoreboard = redisDb.GetScoreboard();
            var toShow = scoreboard.Select(x => new RecipeVote()
            {
                Name = x.Key,
                Score = x.Value
            }).ToList();
            await Clients.All.SendAsync("ReceiveScoreboar", toShow);
        }
    }
}