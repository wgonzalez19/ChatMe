namespace ChatMe.Application.Messages.Hub
{
    using System.Threading.Tasks;

    public interface IChatHub
    {
        Task InformClient(MessageDto message);
    }
}
