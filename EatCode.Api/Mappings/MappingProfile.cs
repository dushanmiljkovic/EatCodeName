using AutoMapper;
using Models.Domein;
using Models.DTO;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();

            CreateMap<Nutrition, NutritionDTO>();
            CreateMap<NutritionDTO, Nutrition>();

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>(); 

            CreateMap<CreateRecipeRequestModel, RecipeDTO>(); 
            CreateMap<RecipeDTO, CreateRecipeRequestModel>(); 

            CreateMap<UpdateRecipeRequestModel, RecipeDTO>(); 
            CreateMap<RecipeDTO, UpdateRecipeRequestModel>();  
        }
    }
}
