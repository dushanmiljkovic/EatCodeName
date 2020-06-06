using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatCode.Api.Neo4J;
using EatCode.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace EatCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedController : ControllerBase
    {
        private readonly IMatrixService matrixService;
        public RelatedController(IMatrixService matrixService)
        {
            this.matrixService = matrixService;
        }

        [HttpGet("create-dishe")]
        public async Task<IActionResult> CreateRecipe()
        {
            var dish = new DisheDTO
            { 
                Name = "Kacamak",
                Season = "Every"
            };

            var drink = new DrinkDTO
            {
                Name = "Mleko", 
            };

            var dishId = matrixService.CreateDishe(dish);
            var drinkId = matrixService.CreateDrink(drink);

            var finalResult = matrixService.RelateDisheDrink(dishId, drinkId, Models.Domein.DisheDrink.GoesWith);
            var finalResult2 = matrixService.GetSpecificDishWithGoesWithDrinks(dishId ); 

            return Ok(finalResult2);
        }
         
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var dish = new DisheDTO
            {
                Name = "Drobeno",
                Season = "Every"
            }; 

            var dishId = matrixService.CreateDishe(dish);
            var drinkId = "4b4c1036-448f-43c5-91a7-922eebc00aef";

            var finalResult = matrixService.RelateDisheDrink(dishId, drinkId, Models.Domein.DisheDrink.GoesWith);

            return Ok(finalResult);
        }

        [HttpGet("test2")]
        public async Task<IActionResult> Test2()
        {
            var dish = new DisheDTO
            {
                Name = "Kupus",
                Season = "Every"
            };

            var dishId = matrixService.CreateDishe(dish);
            var drinkId = "4b4c1036-448f-43c5-91a7-922eebc00aef";

            var finalResult = matrixService.RelateDisheDrink(dishId, drinkId, Models.Domein.DisheDrink.Never);

            return Ok(finalResult);
        }
    }
}