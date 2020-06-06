using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Stack
{
    public class DefaultStack
    {
        readonly RedisClient redis = new RedisClient(Config.SingleHost);

        public DefaultStack()
        {

        }

        public void Test()
        {
            try
            {
                var redisTest = redis.As<RecipeLinking>();

                var test = new RecipeLinking { DishId = "1", RecipeId = "1", Id = redisTest.GetNextSequence() };

                redisTest.Store(test);
            }
            catch (Exception ex)
            {
                var log = ex;
            }
        }

        public IList<RecipeLinking> Test2()
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

        public void Test4()
        {
            try
            {
                string value = "Grasak";
                byte[] stringToByte = Encoding.ASCII.GetBytes(value);
                var test = redis.ZAdd("RecipeRankings", 1, stringToByte);
            }
            catch(Exception ex)
            {

            }
        }

        public List<string> Test5()
        {
            try
            {
              
                var test = redis.GetAllItemsFromSortedSet("RecipeRankings");
                return test;
            }
            catch (Exception ex)
            {
                var test = ex.ToString();
                return null;
            }
        }
    }
    public class RecipeLinking
    {
        public long Id { get; set; }
        public string RecipeId { get; set; }
        public string DishId { get; set; }

    } 
}
