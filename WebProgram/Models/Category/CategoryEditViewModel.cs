using System.ComponentModel.DataAnnotations;

namespace WebProgram.Models.Category
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва категорії - Редагування")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Опис - Редагування")]
        public string? Description { get; set; } = string.Empty;
        
        [Display(Name = "Ваше фото - Редагування")]
        public IFormFile ImageFile { get; set; } = null!;
    }
}
