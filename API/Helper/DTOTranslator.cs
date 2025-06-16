using Core.Models;
using API.DTOModels;

namespace API.Helper
{
    internal static class DTOTranslator
    {
        //TODO: Change values for the DTO's and the domain models

        // Company
        public static CompanyDTO CompanyToDTO(Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                CEOId = company.CEOId
            };
        }

        public static Company CompanyFromDTO(CompanyDTO companyDTO)
        {
            return new Company
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                CEOId = companyDTO.CEOId
            };
        }

        // Article
        public static ArticleResponseDTO ToArticleResponseDTO(Article article)
        {
            return new ArticleResponseDTO
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Content = article.Source.Content
            };
        }

        public static Article ToArticleModel(CreateArticleRequestDTO createArticleRequestDTO)
        {
            return new Article
            {
                Title = createArticleRequestDTO.Title,
                Source = new Source { Content = createArticleRequestDTO.Content }
            };
        }

        // Comments
        public static CommentDTO ToCommentDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                ArticleId = comment.ArticleId
            };
        }

        public static Comment ToCommentModel(CreateCommentDTO createCommentDTO)
        {
            return new Comment
            {
                Content = createCommentDTO.Text,
                ArticleId = createCommentDTO.ArticleId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
