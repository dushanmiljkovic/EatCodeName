using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatCode.Api.Neo4J;
using EatCode.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
         
        [HttpGet("test")]
        public async Task<IActionResult> CreateRecipe()
        {
            matrixService.Test();
            return Ok();
        }
    }
}