using Core.Models;
using DAL.Entities;

namespace DAL.Helper
{
    internal static class ArticleEntityTranslator
    {
        public static ArticleEntity ToEntity(Article article)
        {
            return new ArticleEntity
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Source = new SourceEntity
                {
                    Content = article.Source.Content,
                }
            };
        }

        public static Article ToModel(ArticleEntity articleEntity)
        {
            return new Article
            {
                Id = articleEntity.Id,
                Title = articleEntity.Title,
                Summary = articleEntity.Summary,
                Source = new Source
                {
                    Content = articleEntity.Source.Content,
                }
            };
        }
    }
}
