using Models.Domein;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.Api.Services
{
    public interface IMatrixService
    {
        string CreateDishe(DisheDTO model);
        string CreateDrink(DrinkDTO model);
        bool RelateDisheDrink(string disheId, string drinkId, DisheDrink relation);
        (Dishe, List<Drink>) GetSpecificDishWithGoesWithDrinks(string id);
    }
}
