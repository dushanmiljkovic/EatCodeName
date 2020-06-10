using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redis.Stack
{
    public class ChatStack
    {
        private readonly RedisClient redis = new RedisClient(Config.SingleHost);
        private readonly string chatHistoryID = "chatHistory";

        public ChatStack(IOptions<RedisSettings> settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Value.ConnectionString)) { throw new Exception("Missing setting"); }

            redis = new RedisClient(settings.Value.ConnectionString);
        }

        public bool AddItemToChatHistory(string item)
        {
            try
            {
                //redis.AddItemToList(chatHistoryID, item);
                byte[] stringToByte = Encoding.ASCII.GetBytes(item);
                var result = redis.RPush(chatHistoryID, stringToByte);

                return true;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return false;
            }
        }

        public bool DeleteChatHistory()
        {
            try
            {
                redis.Delete(chatHistoryID);
                return true;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return false;
            }
        }

        public List<string> GetChatHistory()
        {
            try
            {
                return redis.GetAllItemsFromList(chatHistoryID);
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return null;
            }
        }

        public List<string> GetNMsgHistory(int n)
        {
            try
            {
                var count = (int)redis.LLen(chatHistoryID);
                if (count == 0) { return new List<string>(); }
                return redis.GetAllItemsFromList(chatHistoryID).Skip(Math.Max(0, count - n)).ToList();
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return null;
            }

        }
    }
}
