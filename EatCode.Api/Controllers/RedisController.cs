using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Stack;

namespace EatCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly DefaultStack redisCliten;
        public RedisController()
        {
            redisCliten = new DefaultStack();
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            //redisCliten.Test();
            //var tez = redisCliten.Test2();

            redisCliten.Test4();
            var rez = redisCliten.Test5();

            return Ok(rez);
        }
    }
}