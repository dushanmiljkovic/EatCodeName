using Models.Domein;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Stack
{
    public class DefaultStack
    {
        readonly RedisClient redis = new RedisClient(Config.SingleHost);
        readonly string recipeRankings = "RecipeRankings";

        public DefaultStack() {  }

        public void InserRecipeLink(string dishId, string recipeId)
        {
            try
            {
                var redisTest = redis.As<RecipeLinking>();

                var test = new RecipeLinking { DishId = dishId, RecipeId = recipeId, Id = redisTest.GetNextSequence() };

                redisTest.Store(test);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }

        public IList<RecipeLinking> GetAllRecipeLinking()
        {
            try
            {
                var redisTest = redis.As<RecipeLinking>();
                return redisTest.GetAll();
            }
            catch (Exception ex)
            {
                var log = ex;
                return null;
            }
        }

        public void InsertString(string value)
        {
            try
            { 
                byte[] stringToByte = Encoding.ASCII.GetBytes(value);
                var test = redis.ZAdd(recipeRankings, 1, stringToByte);
            }
            catch(Exception ex)
            {
                var log = ex.ToString();
            }
        }

        public List<string> GetAllRecipeRanks()
        {
            try
            {
              
                var status = redis.GetAllItemsFromSortedSet("RecipeRankings");
                return status;
            }
            catch (Exception ex)
            {
                var log = ex.ToString();
                return null;
            }
        }
    } 
}
