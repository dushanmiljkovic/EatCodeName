using EatCode.SignalR.Services;
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
        private readonly IScoreboardService scoreboardService;
        public ScoreboardHub(IScoreboardService scoreboardService)
        {
            this.scoreboardService = scoreboardService;
        }
        public async Task ShowScoreboar()
        {
          
            await Clients.All.SendAsync("ReceiveScoreboar", scoreboardService.GetScoreboarFlat().OrderByDescending(o => o.Score).ToList());
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveScoreboar", scoreboardService.GetScoreboarFlat().OrderByDescending(o => o.Score).ToList());
        } 
    }
}