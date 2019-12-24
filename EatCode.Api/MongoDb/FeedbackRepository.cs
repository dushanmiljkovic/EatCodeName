﻿using Microsoft.Extensions.Configuration;
using System;

namespace EatCode.Api.MongoDb
{
    public class FeedbackRepository : MongoCRUD
    {
        private string tableName;
        public FeedbackRepository(IConfiguration configuration)
            : base(configuration.GetValue<string>("MongoDb:FoodConnection"),
                  configuration.GetValue<string>("MongoDb:Database"),
                  configuration.GetValue<string>("MongoDb:TableName"))
        {
            //SeedDB();
            this.tableName = configuration.GetValue<string>("MongoDb:TableName");
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
