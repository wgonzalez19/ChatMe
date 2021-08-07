namespace ChatMe.Application.Messages.SendMessage
{
    using ChatMe.Application.Configuration.Commands;
    using ChatMe.Application.Configuration.Persistence;
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Domain.Messages;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
    {
        private readonly IMediator mediator;

        public SendMessageCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }


    }
}
