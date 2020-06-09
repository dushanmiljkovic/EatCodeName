using Models.DTO;
using System;
using System.Collections.Generic;

namespace EatCode.Api.Services
{
    public interface IRecipeService
    {
        bool CreateRecipe(RecipeDTO model);

        RecipeDTO GetRecipe(Guid id);

        List<RecipeDTO> GetRecipes();

        bool UpdateRecipe(RecipeDTO model);

        bool DeleteRecipe(Guid id);
    }
}