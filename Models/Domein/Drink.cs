using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Domein
{
    public class Drink
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public DrinkType Type { get; set; }
        public int AlcoholLevel { get; set; }
    }
}
