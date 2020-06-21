using Models.Domein;
using Models.DTO;
using System.Collections.Generic;

namespace EatCode.Api.Services
{
    public interface IMatrixService
    {
        string CreateDishe(DisheDTO model);
        Dishe GetSpecificDish(string id);
        List<Dishe> GetDishs();

        string CreateDrink(DrinkDTO model);

        Drink GetSpecificDrink(string id);
        List<Drink> GetDrinks();

        bool UpdateDrink(DrinkDTO model);

        bool DeleteDrink(string id);

        bool RelateDisheDrink(string disheId, string drinkId, DisheDrink relation);

        (Dishe, long) GetSpecificDishWithGoesWithCount(string id);

        (Dishe, List<Drink>) GetSpecificDishWithGoesWithDrinks(string id);

        (Dishe, long) GetSpecificDishWithNeverCount(string id);

        (Dishe, List<Drink>) GetSpecificDishWithNeverDrinks(string id);
        
        //Delete reation between items 
        bool DeleteRelateDisheDrink(string disheId, string drinkId, DisheDrink relation);
    }
}