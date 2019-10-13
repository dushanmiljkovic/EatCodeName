using System;
using System.Collections.Generic;
using System.Text;

namespace Models.RequestModels
{
    public class IngredientRequestModel
    {
        public string Unit { get; set; }
        public int UnitCount { get; set; }
        public string Name { get; set; }
    }
}
