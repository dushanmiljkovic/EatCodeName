using Models.Enums;

namespace Models.DTO
{
    public class DisheDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public string Origin { get; set; }
        public ServedType ServedType { get; set; }
        public string ServedOnEvents { get; set; }
        public string ExternalLink { get; set; }
    }
}