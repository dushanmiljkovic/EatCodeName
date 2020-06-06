using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisService
{
    public class Crud
    {
        readonly RedisClient redis = new RedisClient(Config.SingleHost);

        public Crud()
        {

        }

        public void Test()
        {
            var redisTest = redis.As<RecipeLinking>();

            var test = new RecipeLinking { DishId = "1", RecipeId = "1", Id = redisTest.GetNextSequence() };

            redisTest.Store(test);
        }

        public void Test2()
        {

        }

    }
    public class RecipeLinking
    {
        public long Id { get; set; }
        public string RecipeId { get; set; }
        public string DishId { get; set; }

    }
    public class Config
    {
        /// <summary>
        ///  localhost:6379
        /// </summary>
        public static string SingleHost
        {
            get { return "localhost"; }
        }
        public const int RedisPort = 6379;
        public static string SingleHostConnectionString
        {
            get
            {
                return SingleHost + ":" + RedisPort;
            }
        }
        public static BasicRedisClientManager BasicClientManger
        {
            get
            {
                return new BasicRedisClientManager(new[] {
                    SingleHostConnectionString
                });
            }
        }
    }
}
