using Microsoft.AspNetCore.SignalR;

namespace SignalR.API.Hubs
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} - {message}");
        }

        public static Func<string, IHubContext<ChatHub, IChatClient>, Task<IResult>> SendMessageToAll()
        {
            return async (string message, IHubContext<ChatHub, IChatClient> context) =>
            {
                await context.Clients.All.ReceiveMessage(message);
                return Results.Ok(message);
            };
        }
    }
}
