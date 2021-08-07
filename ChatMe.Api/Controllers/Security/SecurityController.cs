namespace ChatMe.Api.Controllers.Security
{
    using ChatMe.Api.Controllers.Shared;
    using ChatMe.Application.Users.LoginUser;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : BaseController
    {
        public SecurityController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Login an user.
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginUserRequest request)
        {
            var response = await mediator.Send(new LoginUserCommand(request.Username, request.Password));

            return Ok(response);
        }
    }
}
