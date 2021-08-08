namespace ChatMe.Api.Controllers.Health
{
    using ChatMe.Api.Controllers.Shared;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/ping")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Ping()
        {
            string now = DateTime.Now.ToLongDateString();

            return Ok($"Pong at - {now}");
        }
    }
}
