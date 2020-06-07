using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Stack
{
    public class FeedbackRepository : MongoCRUD
    {
        private string tableName;

        private static string FoodConnection = "mongodb://localhost:27017/";
        private static string Database = "Cookbook";
        private static string  TableName = "Feedback";
         
        public FeedbackRepository()
            : base(FoodConnection,
                  Database,
                  TableName)
        {
            //SeedDB();
            this.tableName = TableName;
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
 