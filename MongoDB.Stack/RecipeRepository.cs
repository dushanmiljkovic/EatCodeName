using Models.Domein;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Stack
{
    public class RecipeRepository : MongoCRUD
    {
        private readonly string tableName;

        private static readonly string FoodConnection = "mongodb://localhost:27017/";
        private static readonly string Database = "Cookbook";
        private static readonly string TableName = "Recipes";


        public RecipeRepository()
            : base(FoodConnection,
                  Database,
                  TableName)
        { 
            this.tableName = TableName;
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
