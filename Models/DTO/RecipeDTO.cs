using Microsoft.AspNetCore.Http;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.DTO
{
    public class RecipeDTO : IBindingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PrepTime { get; set; } //in mins
        public int CookTime { get; set; } //in mins
        public int ReadyIn { get; set; } //in mins
        public CookingSkills SkillRequired { get; set; }
        public int Serves { get; set; }
        public List<string> PreparationMethod { get; set; }
        public NutritionDTO Nutrition { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public string Description { get; set; }
        public string FileId { get; set; }
        public byte[] FileDb { get; set; }
        public IFormFile File { get; set; }
    }
}