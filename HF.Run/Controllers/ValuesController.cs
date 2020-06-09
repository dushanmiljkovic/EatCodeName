using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HF.Run.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //RecurringJob.AddOrUpdate(() => PrintToDebug($@"Hangfire recurring task started - {Guid.NewGuid()}"), Cron.Minutely);

            return new string[] { "value1", "value2" };
        }

        public static void PrintToDebug(string message)
        {
            Console.WriteLine(message);
        }
    }
}