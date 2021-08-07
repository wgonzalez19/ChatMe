using ChatMe.Domain.Messages;

namespace ChatMe.Application.Configuration.Service
{
    public interface IBot
    {
        void PostMessage(Message message);
    }
}
