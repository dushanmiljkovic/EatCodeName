namespace Models.Domein
{
    public class RecipeLinking
    {
        public long Id { get; set; }
        public string RecipeId { get; set; }
        public string DishId { get; set; }
    }
}