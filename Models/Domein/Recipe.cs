using Models.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.Domein
{
    public class Recipe
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? PrepTime { get; set; } //in mins
        public int? CookTime { get; set; } //in mins
        public int? ReadyIn { get; set; } //in mins
        public CookingSkills SkillRequired { get; set; }
        public int? Serves { get; set; }
        public List<string> PreparationMethod { get; set; }
        public Nutrition Nutrition { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
        public string FileId { get; set; }
    }
}
