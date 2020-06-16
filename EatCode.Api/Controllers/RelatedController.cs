using EatCode.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.RequestModels;
using System.Threading.Tasks;

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

        [HttpPost("create-dishe")]
        public async Task<IActionResult> CreateDishe([FromForm] DisheDTO dishe)
        {
            dishe.Id = null;
            var result = matrixService.CreateDishe(dishe);
            if (string.IsNullOrEmpty(result)) { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("dishe/{id}")]
        public async Task<IActionResult> GetDishe(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var result = matrixService.GetSpecificDish(id);
            if (result == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpPost("create-drink")]
        public async Task<IActionResult> CreateDrink([FromForm] DrinkDTO model)
        {
            model.Id = null;
            var result = matrixService.CreateDrink(model);
            if (string.IsNullOrEmpty(result)) { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("drink/{id}")]
        public async Task<IActionResult> GetDrink(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var result = matrixService.GetSpecificDrink(id);
            if (result == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("update-drink/{id}")]
        public async Task<IActionResult> UpdateDrink(DrinkDTO model)
        {
            if (string.IsNullOrEmpty(model.Id)) { return BadRequest(); }

            var result = matrixService.UpdateDrink(model);
            if (!result) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("delete-drink/{id}")]
        public async Task<IActionResult> DeleteDrink(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var result = matrixService.DeleteDrink(id);
            if (!result) { return Conflict(); }

            return Ok(result);
        }

        [HttpPost("relate-drink-dish")]
        public async Task<IActionResult> RelateDisheDrink([FromForm] RelateDisheDrinkRequestModel model)
        {
            if (string.IsNullOrEmpty(model.DisheId)) { return BadRequest(); }
            if (string.IsNullOrEmpty(model.DrinkId)) { return BadRequest(); }

            var result = matrixService.RelateDisheDrink(model.DisheId, model.DrinkId, model.Relation);
            if (!result) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("dish-has-good-drink/{id}")]
        public async Task<IActionResult> DishDrinkLikesCount(string dishId)
        {
            if (string.IsNullOrEmpty(dishId)) { return BadRequest(); }

            var result = matrixService.GetSpecificDishWithGoesWithCount(dishId);
            if (result.Item1 == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("dish-good-drink/{dishId}")]
        public async Task<IActionResult> DishGoodDrinksSuggestion(string dishId)
        {
            if (string.IsNullOrEmpty(dishId)) { return BadRequest(); }

            var result = matrixService.GetSpecificDishWithGoesWithDrinks(dishId);
            if (result.Item1 == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("dish-never-drink/{dishId}")]
        public async Task<IActionResult> DishBadDrinksSuggestion(string dishId)
        {
            if (string.IsNullOrEmpty(dishId)) { return BadRequest(); }

            var result = matrixService.GetSpecificDishWithGoesWithDrinks(dishId);
            if (result.Item1 == null) { return Conflict(); }

            return Ok(result);
        }

        #region Test: Not production Ready

        [HttpGet("create-dishe-test")]
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
            var finalResult2 = matrixService.GetSpecificDishWithGoesWithDrinks(dishId);

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

        #endregion Test: Not production Ready
    }
}