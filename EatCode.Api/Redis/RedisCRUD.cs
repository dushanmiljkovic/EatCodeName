namespace EatCode.Api.Redis
{
    public interface RedisCRUD<T>
    {
        T Get(string key);

        void Save(string key, T obj);

        void Delete(string key);
    }

    public class RedisTest
    {
        public static void Test()
        {
            //var manager = new RedisManagerPool("localhost:6379");
            //using (var client = manager.GetClient())
            //{
            //    client.Set("foo", "bar");
            //    Console.WriteLine("foo={0}", client.Get<string>("foo"));
            //}
        }
    }
}
