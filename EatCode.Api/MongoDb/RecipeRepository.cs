
using Microsoft.Extensions.Configuration;
using Models.Domein;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace EatCode.Api.MongoDb
{
    public class RecipeRepository : MongoCRUD
    {
        private readonly string tableName;
        public RecipeRepository(IConfiguration configuration)
            : base(configuration.GetValue<string>("MongoDb:FoodConnection"),
                  configuration.GetValue<string>("MongoDb:Database"),
                  configuration.GetValue<string>("MongoDb:TableNameRecipes"))
        {

            this.tableName = configuration.GetValue<string>("MongoDb:TableNameRecipes");
            //Seed();
        }

        public override void InsertRecord<T>(T record)
        {
            if (typeof(T) != typeof(Recipe))
            {
                throw new Exception("can insert");
            }

            var collection = _contex.GetCollection<T>(tableName);
            collection.InsertOne(record);
        }
        public override void Seed()
        {
            var r1 = new Recipe()
            {
                Name = "Pasta",

                PrepTime = 1,
                CookTime = 2,
                ReadyIn = 3,
                SkillRequired = CookingSkills.Dishwasher,
                Serves = 3,
                PreparationMethod = new List<string>() { "the", "fox", "jumps", "over", "the", "dog" },
                Nutrition = new Nutrition() { Carbs = 1, Fat = 1 },
                Ingredients = new List<Ingredient>() { new Ingredient() { Name ="Grasak", Unit = "Pesnice", UnitCount=1}
                                                     , new Ingredient() { Name = "Grasak", Unit = "Pesnice", UnitCount = 1 } },
                Description = "Test",
            };

            InsertRecord<Recipe>(r1);
        }
    }
}
