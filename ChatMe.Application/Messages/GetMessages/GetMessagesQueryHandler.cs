namespace ChatMe.Application.Messages.GetMessages
{
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Application.Configuration.Queries;
    using ChatMe.Application.Shared.Queries;
    using ChatMe.Domain.Messages;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessagesRepository messagesRepository;

        public GetMessagesQueryHandler(IMessagesRepository messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            PageOptions pageOptions = new() { Size = request.Size };

            IEnumerable<Message> messages = await this.messagesRepository.GetRange(pageOptions);

            return MapResult(messages).Reverse();
        }

        private IEnumerable<MessageDto> MapResult(IEnumerable<Message> messages) =>
            messages.Select(message => MessageDto.MapFromDomain(message));
    }
}
