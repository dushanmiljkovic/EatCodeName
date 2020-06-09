using ServiceStack.Redis;

namespace Redis.Stack
{
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