namespace API.DTOModels
{
    public class CreateCommentDTO
    {
        public string Text { get; set; } = string.Empty;
        public int ArticleId { get; set; }
    }
}
