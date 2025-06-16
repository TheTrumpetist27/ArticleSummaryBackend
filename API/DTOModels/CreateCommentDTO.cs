namespace API.DTOModels
{
    public class CreateCommentDTO
    {
        public string Content { get; set; } = string.Empty;
        public int ArticleId { get; set; }
    }
}
