using EatCode.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace EatCode.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string botName = "EatBot";
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessageHistory", GetChatHistoryAsList());
        }

        public async Task SendMessage(string user, string message)
        {
             
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(user))
            {
                await Clients.All.SendAsync("ReceiveMessage", "anonimo", "dont be shy", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(user)) { user = "anonimo"; }

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

            switch (messageType)
            {
                case MessageType.Text:
                    {
                        var chatHistoryService = new ChatHistoryService();
                        chatHistoryService.AddToHistory(message);
                        // Send msg to chat
                        await Clients.All.SendAsync("ReceiveMessage", user, message, true);
                        break;
                    }
                case MessageType.BotCommand:
                    {
                        var chatHistoryService = new ChatHistoryService();
                        chatHistoryService.AddToHistory(message);

                        var scoreboardService = new ScoreboardService();
                        var messageParsed = ParseMessage(user, message, scoreboardService);

                        // Send msg to chat
                        await Clients.All.SendAsync("ReceiveMessage", user, messageParsed.Item2, messageParsed.Item1);

                        // Update scoreboard
                        var toShow = scoreboardService.GetScoreboarFlat().OrderByDescending(o => o.Score);
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

                var score = CastStringToDouble(parse.Length == 3 ? parse[2].Trim() : "1");
                if (score > 10) { score = 10; }

                var votedResult = service.AddVote(recipe, score);
                if (votedResult)
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
                if (parse[1].Trim() == "test123")
                {
                    var status = service.StoreVotes(user);
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

        private List<string> GetChatHistoryAsList()
        {
            var chatHistoryService = new ChatHistoryService();
            var historyTest = chatHistoryService.GetChatHistory(10);
            return historyTest;
        }

        // Cast Vote from string to double, if its invalid string by default it will be casted to 1
        private static double CastStringToDouble(string score)
        {
            try
            {
                return Convert.ToDouble(score);
            }
            catch
            {
                return 1;
            }
        }

        private enum MessageType
        {
            NotSupported,
            Text,
            BotCommand
        }

        private enum BotCommand
        {
            Vote,
            DeleteVote,
            ForceStore,
        }
    }
}