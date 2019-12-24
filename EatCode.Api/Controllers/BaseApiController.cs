using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EatCode.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public async Task<IActionResult> CreateResponse<T, F>(string data, Func<T, F> factory, Func<string, Task<T>> service)
        {
            var stringTest = data + "Novi Text";
            var result = await service(stringTest);
            var newObj = factory(result);
            return Ok(newObj);
        }
    }
}