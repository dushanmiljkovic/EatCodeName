namespace Models.DTO
{
    public class IngredientDTO : IBindingModel
    {
        public string Unit { get; set; }
        public int UnitCount { get; set; }
        public string Name { get; set; }
    }
}