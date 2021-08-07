namespace ChatMe.Application.Configuration.Service
{
    using ChatMe.Application.Messages.SendMessage.Events;
    using System.Threading.Tasks;

    public interface IBot
    {
        Task PostMessage(BotMessage message);

        Task ReceiveMessage(string message);
    }
}
