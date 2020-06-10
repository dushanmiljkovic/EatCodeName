namespace Settings
{
    public abstract class IMongoDbSettings
    {
        public string Connection { get; set; }
        public string Database { get; set; }
    }

    public class RecipesMongoDbSettings : IMongoDbSettings
    {
        public string TableNameRecipes { get; set; }
    }
    public class FilesMongoDbSettings : IMongoDbSettings
    {
        public string TableNameFiles { get; set; }
    }

    public class ScoreboardMongoDbSettings : IMongoDbSettings
    {
        public string TableNameScoreboard { get; set; }
    }
    public class FeedbackMongoDbSettings : IMongoDbSettings
    {
        public string TableNameFeedback { get; set; }
    } 
}