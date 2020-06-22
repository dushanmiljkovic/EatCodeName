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
        public async Task<IActionResult> CreateDishe(DisheDTO dishe)
        {
            var result = matrixService.CreateDishe(dishe);
            if (string.IsNullOrEmpty(result)) { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("dishe/all")]
        public async Task<IActionResult> GetDishs()
        {
            var result = matrixService.GetDishs();
            if (result == null) { return Conflict(); }

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
        public async Task<IActionResult> CreateDrink(DrinkDTO model)
        {
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

        [HttpGet("drink/all")]
        public async Task<IActionResult> GetDrinks()
        {
            var result = matrixService.GetDrinks();
            if (result == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpPost("update-drink/{id}")]
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
        public async Task<IActionResult> RelateDisheDrink(RelateDisheDrinkRequestModel model)
        {
            if (string.IsNullOrEmpty(model.DisheId)) { return BadRequest(); }
            if (string.IsNullOrEmpty(model.DrinkId)) { return BadRequest(); }

            var result = matrixService.RelateDisheDrink(model.DisheId, model.DrinkId, model.Relation);
            if (!result) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("dish-has-good-drink/{dishId}")]
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
            if (result.Item1 == null && result.Item2 == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpGet("dish-never-drink/{dishId}")]
        public async Task<IActionResult> DishBadDrinksSuggestion(string dishId)
        {
            if (string.IsNullOrEmpty(dishId)) { return BadRequest(); }

            var result = matrixService.GetSpecificDishWithNeverDrinks(dishId);
            if (result.Item1 == null && result.Item2 == null) { return Conflict(); }

            return Ok(result);
        }

        [HttpPost("derelate-drink-dish")]
        public async Task<IActionResult> DerelateDisheDrink(RelateDisheDrinkRequestModel model)
        {
            if (string.IsNullOrEmpty(model.DisheId)) { return BadRequest(); }
            if (string.IsNullOrEmpty(model.DrinkId)) { return BadRequest(); }

            var result = matrixService.DeleteRelateDisheDrink(model.DisheId, model.DrinkId, model.Relation);
            if (!result) { return Conflict(); }

            return Ok(result);
        }
    }
}