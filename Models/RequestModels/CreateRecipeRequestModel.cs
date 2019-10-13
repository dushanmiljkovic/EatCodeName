﻿
using Microsoft.AspNetCore.Http;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.RequestModels
{
    public class CreateRecipeRequestModel
    {
        public string Name { get; set; }
        public int PrepTime { get; set; } //in mins
        public int CookTime { get; set; } //in mins
        public int ReadyIn { get; set; } //in mins
        public CookingSkills SkillRequired { get; set; }
        public int Serves { get; set; }
        public List<string> PreparationMethod { get; set; }
        public NutritionRequestModel Nutrition { get; set; }
        public List<IngredientRequestModel> Ingredients { get; set; }
        public string Description { get; set; }  
        public IFormFile File { get; set; }
    }
}
