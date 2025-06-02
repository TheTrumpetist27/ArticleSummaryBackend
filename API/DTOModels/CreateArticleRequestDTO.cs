namespace API.DTOModels
{
    public class CreateArticleRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // source content
    }
}
