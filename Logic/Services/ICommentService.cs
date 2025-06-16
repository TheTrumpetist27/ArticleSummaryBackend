using Core.Models;

namespace Core.Services
{
    public interface ICommentService
    {
        Task<Comment> AddCommentAsync(Comment comment);
        Task<List<Comment>> GetCommentsForArticleAsync(int articleId);
    }
}
