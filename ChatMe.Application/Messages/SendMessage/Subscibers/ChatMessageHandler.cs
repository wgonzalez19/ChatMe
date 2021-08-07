namespace ChatMe.Application.Messages.SendMessage.Subscibers
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Messages.Hub;
    using ChatMe.Application.Messages.SendMessage.Events;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Messages;
    using ChatMe.Domain.Users;
    using ChatMe.Resources;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class ChatMessageHandler : INotificationHandler<ChatMessage>
    {
        private readonly IMessagesRepository messagesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IHubContext<ChatHub, IChatHub> hub;

        public ChatMessageHandler(IMessagesRepository messagesRepository, IUsersRepository usersRepository, IHubContext<ChatHub, IChatHub> hub)
        {
            this.messagesRepository = messagesRepository;
            this.usersRepository = usersRepository;
            this.hub = hub;
        }

        public async Task Handle(ChatMessage notification, CancellationToken cancellationToken)
        {
            User user = await this.usersRepository.SingleOrDefault(notification.Username);

            Throw.When<RestException>(
                user is null,
                ExceptionMessage.USER_NOT_FOUND,
                HttpStatusCode.NotFound
            );

            var messagePosted = new Message(notification.MessageText, user);

            var savedMessage = await this.messagesRepository.Add(messagePosted);

            Throw.When<Exception>(savedMessage is null, ExceptionMessage.SAVE_MESSAGE_FAILED);

            await hub.Clients.All.InformClient(MessageDto.MapFromDomain(savedMessage));
        }
    }
}
