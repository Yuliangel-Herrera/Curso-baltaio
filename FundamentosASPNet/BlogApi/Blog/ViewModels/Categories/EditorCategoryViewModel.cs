using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é obrigatorio")]
        public string Slug { get; set; }
    }
}
