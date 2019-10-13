using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class NutritionDTO
    {
        public int Kcal { get; set; }
        public int Fat { get; set; } //in grams
        public int Saturates { get; set; }
        public int Carbs { get; set; }
        public int Sugars { get; set; }
        public int Fibre { get; set; }
        public int Protein { get; set; }
        public int Salt { get; set; }
    }
}
