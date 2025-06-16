using Core.Models;

namespace Core.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddCommentAsync(Comment comment);
        Task<List<Comment>> GetCommentsForArticleAsync(int articleId);
        Task<bool> DeleteCommentAsync(int commentId); // Method to delete a comment
    }
}
