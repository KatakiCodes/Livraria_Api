using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="O título da categoria deve ser informado obrigatóriamente!")]
        [MaxLength(60, ErrorMessage = "O título da categoria deve contér no máximo 60 caractéres!")]
        [MinLength(2, ErrorMessage = "O título da categoria deve contér no mínimo 2 caractéres!")]
        public string  Title { get; set; } = string.Empty;
    }
}
