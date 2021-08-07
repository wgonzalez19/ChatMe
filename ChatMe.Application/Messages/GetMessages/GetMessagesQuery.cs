namespace ChatMe.Application.Messages.GetMessages
{
    using ChatMe.Application.Configuration.Queries;
    using System.Collections.Generic;

    public class GetMessagesQuery : IQuery<IEnumerable<MessageDto>>
    {
        public GetMessagesQuery(int? size)
        {
            this.Size = size ?? 50;
        }

        public int Size { get; }
    }
}
