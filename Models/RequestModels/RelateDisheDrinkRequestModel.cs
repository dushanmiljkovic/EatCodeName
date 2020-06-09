using Models.Domein;

namespace Models.RequestModels
{
    public class RelateDisheDrinkRequestModel
    {
        public string DisheId { get; set; }
        public string DrinkId { get; set; }
        public DisheDrink Relation { get; set; }
    }
}