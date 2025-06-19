using System.ComponentModel.DataAnnotations;

namespace API.DTOModels
{
    public class CreateArticleRequestDTO
    {
        [Required(ErrorMessage = "Title is verplicht.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Content is verplicht.")]
        public string Content { get; set; } = string.Empty; // source content
    }
}
