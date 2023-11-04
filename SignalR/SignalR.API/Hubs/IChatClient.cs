namespace SignalR.API.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
