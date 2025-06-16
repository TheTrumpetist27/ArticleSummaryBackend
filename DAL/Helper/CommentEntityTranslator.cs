using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class CommentEntityTranslator
    {
        public static CommentEntity ToEntity(Comment comment)
        {
            return new CommentEntity
            {
                Id = comment.Id,
                Content = comment.Content,
                ArticleId = comment.ArticleId,
                CreatedAt = comment.CreatedAt
            };
        }
        public static Comment ToModel(CommentEntity commentEntity)
        {
            return new Comment
            {
                Id = commentEntity.Id,
                Content = commentEntity.Content,
                ArticleId = commentEntity.ArticleId,
                CreatedAt = commentEntity.CreatedAt
            };
        }
    }
}
