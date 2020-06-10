using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using Settings;
using System;
using System.Collections.Generic;

namespace Redis.Stack
{
    public class ScoreboardStack
    {
        private readonly RedisClient redis; //= new RedisClient(Config.SingleHost);
        private readonly string scoreboardId = "recipeScoreboard";

        public ScoreboardStack(IOptions<RedisSettings> settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Value.ConnectionString)) { throw new Exception("Missing setting"); }

            redis = new RedisClient(settings.Value.ConnectionString);
        }

        public bool AddItemToScoreboard(string item, double score = 0)
        {
            try
            {
                var itemExists = redis.SortedSetContainsItem(scoreboardId, item);
                if (!itemExists)
                {
                    return redis.AddItemToSortedSet(scoreboardId, item, score);
                }
                _ = redis.IncrementItemInSortedSet(scoreboardId, item, score);
                return true;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return false;
            }
        }

        public IDictionary<string, double> GetScoreboard()
        {
            try
            {
                return redis.GetAllWithScoresFromSortedSet(scoreboardId);
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return null;
            }
        }

        public bool DeleteItemFromScoreboard(string item)
        {
            try
            {
                redis.RemoveItemFromSet(scoreboardId, item);
                return true;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return false;
            }
        }

        public bool DeleteScoreboard()
        {
            try
            {
                var status = redis.RemoveRangeFromSortedSet(scoreboardId, 0, -1);
                return true;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return false;
            }
        }
    }
}