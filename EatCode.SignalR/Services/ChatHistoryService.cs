using Redis.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Services
{
    public class ChatHistoryService
    {
        private readonly ChatStack chatStack;

        public ChatHistoryService()
        {
            chatStack = new ChatStack();
        }

        public bool AddToHistory(string msg)
        {
            try
            {
                return chatStack.AddItemToChatHistory(msg);
            }
            catch (Exception ex)
            {
                var log = ex;
                return false;
            }
        }
        public List<string> GetChatHistory()
        {
            try
            {
                return chatStack.GetChatHistory();
            }
            catch (Exception ex)
            {
                var log = ex;
                return null;
            }
        }

        public List<string> GetChatHistory(int n)
        {
            try
            {
                if (n <= 0) { return null; }

                return chatStack.GetNMsgHistory(n);
            }
            catch (Exception ex)
            {
                var log = ex;
                return null;
            }
        }
    }
}
