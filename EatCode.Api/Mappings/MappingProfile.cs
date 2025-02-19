﻿using AutoMapper;
using Models.Domein;
using Models.DTO;
using Models.RequestModels;

namespace EatCode.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();

            CreateMap<IngredientRequestModel, IngredientDTO>();
            CreateMap<IngredientDTO, IngredientRequestModel>();

            CreateMap<Nutrition, NutritionDTO>();
            CreateMap<NutritionDTO, Nutrition>();
             
            CreateMap<NutritionRequestModel, NutritionDTO>();
            CreateMap<NutritionDTO, NutritionRequestModel>();

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>();

            CreateMap<CreateRecipeRequestModel, RecipeDTO>();
            CreateMap<RecipeDTO, CreateRecipeRequestModel>();

            CreateMap<UpdateRecipeRequestModel, RecipeDTO>();
            CreateMap<RecipeDTO, UpdateRecipeRequestModel>();

            CreateMap<DisheDTO, Dishe>();
            CreateMap<Dishe, DisheDTO>();

            CreateMap<DrinkDTO, Drink>();
            CreateMap<Drink, DrinkDTO>();
        }
    }
}