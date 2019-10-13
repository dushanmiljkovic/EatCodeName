using AutoMapper;
using EatCode.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.RequestModels;
using System;
using System.Threading.Tasks;

namespace EatCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService recipeService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper, IFileService fileService)
        {
            this.recipeService = recipeService;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRecipe([FromForm]CreateRecipeRequestModel request)
        {
            var mapped = mapper.Map<RecipeDTO>(request);
            var imgId = await fileService.Upload(request.File);
            if (string.IsNullOrWhiteSpace(imgId)) { return Conflict(); }
            mapped.FileId = imgId;
            var result = recipeService.CreateRecipe(mapped);
            if (!result) { return Conflict(); }
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdateRecipe(UpdateRecipeRequestModel request)
        {
            var mapped = mapper.Map<RecipeDTO>(request);
            var result = recipeService.UpdateRecipe(mapped);
            if (!result) { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("{guide}")]
        public IActionResult GetRecipe(string guide)
        {
            var id = new Guid(guide);
            var result = recipeService.GetRecipe(id);
            if (result == null) { return Conflict(); }
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var result = recipeService.GetRecipes();
            foreach (var item in result)
            {
                item.FileDb = await fileService.DownloadAsyncAsByte(item.FileId);
            }
            return Ok(result);
        }
         
        [HttpPost("delete")]
        public IActionResult DeleteRecipe(string guide)
        {
            var id = new Guid(guide);
            var result = recipeService.DeleteRecipe(id);
            if (!result) { return Conflict(); }
            return Ok(result);
        }
    }
}