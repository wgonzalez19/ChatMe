namespace ChatMe.Api.Controllers.Message
{
    using ChatMe.Api.Controllers.Shared;
    using ChatMe.Application.Messages;
    using ChatMe.Application.Messages.GetMessages;
    using ChatMe.Application.Messages.SendMessage;
    using ChatMe.Application.Users;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : BaseController
    {
        public MessageController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PostMessage([FromBody]SendMessageRequest request)
        {
            UserDto user = (UserDto)HttpContext.Items[nameof(User)];

            await mediator.Send(new SendMessageCommand(request.MessageText, user.Username));

            return NoContent();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMessages([FromQuery]int size)
        {
            IEnumerable<MessageDto> messages = await mediator.Send(new GetMessagesQuery(size));

            return Ok(messages);
        }
    }
}
