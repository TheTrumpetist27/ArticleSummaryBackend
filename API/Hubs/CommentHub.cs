using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class CommentHub : Hub
    {
        public async Task BroadcastDelete(int commentId)
        {
            await Clients.All.SendAsync("DeleteComment", commentId);
        }
    }
}
