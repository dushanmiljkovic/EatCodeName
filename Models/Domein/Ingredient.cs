using MongoDB.Bson.Serialization.Attributes;

namespace Models.Domein
{
    public class Ingredient
    {
        [BsonIgnore]
        public string Id { get; set; }

        public string Unit { get; set; }
        public int? UnitCount { get; set; }
        public string Name { get; set; }
    }
}