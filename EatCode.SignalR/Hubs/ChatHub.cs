using EatCode.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Models.Domein;
using Redis.Stack;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string botName = "EatBot";
        public async Task SendMessage(string user, string message)
        {
            if(string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(user))
            {  
                await Clients.All.SendAsync("ReceiveMessage", "", "dont be shy", true);
            }
             
            var messageType = MessageType.Text; 

            var supportedCommands = new List<string>() { BotCommand.DeleteVote.ToString(), BotCommand.ForceStore.ToString(), BotCommand.Vote.ToString() };
            foreach (var command in supportedCommands)
            {
                if (message.Contains(command))
                {
                    messageType = MessageType.BotCommand;
                    break;
                } 
            }

            switch (messageType) {

                case MessageType.NotSupported:
                {
                    break;
                } 
                case MessageType.Text:
                {
                    // Send msg to chat
                    await Clients.All.SendAsync("ReceiveMessage", user, message, true);
                    break;
                }
                case MessageType.BotCommand:
                {
                    var scoreboardService = new ScoreboardService();
                    var messageParsed = ParseMessage(user, message, scoreboardService);

                    // Send msg to chat
                    await Clients.All.SendAsync("ReceiveMessage", user, messageParsed.Item2, messageParsed.Item1);

                    // Update scoreboard 
                    var toShow = scoreboardService.GetScoreboarFlat();
                    await Clients.All.SendAsync("ReceiveScoreboar", toShow);

                    break;
                }
                default: 
                    break; 
            }; 
        }

        private (bool, string) ParseMessage(string user, string message, ScoreboardService service)
        {
            var messageToDisplay = ""; 
            var parse = message.Split('/'); 

            if (parse.First().Trim() == BotCommand.Vote.ToString() && parse.Length >= 2)
            {
                var recipe = parse[1].Trim();
                var score = parse.Length == 3 ? parse[2].Trim() : "0";

                var votedResult = service.AddVote(recipe, score);
                if(votedResult)
                {
                    messageToDisplay = botName + ": " + user + " voted for " + recipe;
                }
                else
                {
                    messageToDisplay = botName + ": " + user + "faild to voted for " + recipe + "... :(";
                } 
                return (votedResult, messageToDisplay);
            } 
            else if (parse.First().Trim() == BotCommand.DeleteVote.ToString() && parse.Length == 2)
            {
                var recipe = parse[1].Trim();

                var votedResult = service.RemoveVote(recipe);
                if (votedResult)
                {
                    messageToDisplay = botName + ": " + user + " deleted voted for " + recipe;
                }
                else
                {
                    messageToDisplay = botName + ": " + user + "faild to deleted voted for " + recipe + "... :(";
                }
                return (votedResult, messageToDisplay);
            } 
            else if (parse.First().Trim() == BotCommand.ForceStore.ToString() && parse.Length == 2)
            {
                if (parse[1].Trim() == "admin")
                {
                    var status = service.StoreVores();
                    if (status)
                    {
                        messageToDisplay = botName + ": " + user + " stored the Scoreboard...";
                    }
                    else
                    {
                        messageToDisplay = botName + ": " + user + "faild to stored the Scoreboard";
                    }
                    return (status, messageToDisplay);
                }
            }

            return (true, messageToDisplay);
        }

        enum MessageType
        {
            NotSupported,
            Text,
            BotCommand
        }

        enum BotCommand
        {
            Vote,
            DeleteVote,
            ForceStore,
        } 
    }
}
