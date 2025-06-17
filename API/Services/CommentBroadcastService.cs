using API.Hubs;
using Core.Services;
using Microsoft.AspNetCore.SignalR;
using Core.Models;
using static API.Helper.DTOTranslator;

namespace API.Services
{
    public class CommentBroadcastService : ICommentBroadcastService
    {
        private readonly IHubContext<CommentHub> _hubContext;

        public CommentBroadcastService(IHubContext<CommentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastComment(Comment comment)
        {
            var dto = ToCommentDTO(comment);
            await _hubContext.Clients.All.SendAsync("ReceiveComment", dto);
        }

        public async Task BroadcastDelete(int commentId)
        {
            await _hubContext.Clients.All.SendAsync("DeleteComment", commentId);
        }
    }
}
