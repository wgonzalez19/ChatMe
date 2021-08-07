namespace ChatMe.Application.Messages.Hub
{
    using ChatMe.Domain.Messages;
    using System.Threading.Tasks;

    public interface IChatHub
    {
        Task InformClient(Message message);
    }
}
