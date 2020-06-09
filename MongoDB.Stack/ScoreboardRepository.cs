using Models.Domein;
using System;

namespace MongoDB.Stack
{
    public class ScoreboardRepository : MongoCRUD
    {
        private string tableName;

        private static string FoodConnection = "mongodb://localhost:27017/";
        private static string Database = "Cookbook";
        private static string TableName = "Scoreboards";

        public ScoreboardRepository()
            : base(FoodConnection,
                  Database,
                  TableName)
        {
            //SeedDB();
            this.tableName = TableName;
        }

        public override void InsertRecord<T>(T record)
        {
            if (typeof(T) != typeof(ScoreboeadTable))
            {
                throw new Exception("its not a Scoreboead");
            }

            var collection = _contex.GetCollection<T>(tableName);
            collection.InsertOne(record);
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }
    }
}