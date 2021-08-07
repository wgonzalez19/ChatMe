namespace ChatMe.Application.Messages.SendMessage
{
    using ChatMe.Application.Configuration.Commands;
    using ChatMe.Application.Messages.SendMessage.Events;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
    {
        private readonly IMediator mediator;

        private Dictionary<MessageType, Action<SendMessageCommand>> messageHandlers = new();

        public SendMessageCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;

            messageHandlers.Add(MessageType.Chat, ChatMessageHandler);
            messageHandlers.Add(MessageType.Bot, BotMessageHandler);
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            this.messageHandlers[request.Type](request);

            return new Unit();
        }

        private async void ChatMessageHandler(SendMessageCommand request)
        {
            await this.mediator.Publish(new ChatMessage(request.MessageText, request.Username));
        } 

        private async void BotMessageHandler(SendMessageCommand request)
        {
            await this.mediator.Publish(new BotMessage(request.MessageText));
        }
    }
}
