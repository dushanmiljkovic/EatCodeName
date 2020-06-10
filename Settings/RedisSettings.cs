namespace Settings
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string ConnectionString => this.Host + ":" + this.Port;
    }
}