using Core.Models;

namespace Core.Services
{
    public interface ICommentBroadcastService
    {
        Task BroadcastComment(Comment comment);
        Task BroadcastDelete(int commentId);
    }
}
