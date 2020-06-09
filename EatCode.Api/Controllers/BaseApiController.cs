using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EatCode.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Base Call
        /// </summary>
        /// <typeparam name="T">Service</typeparam>
        /// <typeparam name="F">Factory for models</typeparam>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateResponse<T, F>(string data, Func<T, F> factory, Func<string, Task<T>> service)
        {
            var stringTest = data;
            var result = await service(stringTest);
            var newObj = factory(result);
            return Ok(newObj);
        }

        public async Task<IActionResult> CreateResponse<T, F>(string data, Func<string, Task<T>> service)
        {
            var stringTest = data;
            var result = await service(stringTest);
            return Ok(result);
        }
    }
}