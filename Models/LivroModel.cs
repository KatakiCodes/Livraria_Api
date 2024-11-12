using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LivrariaAPI.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O título do livro deve ser informado obrigatóriamente!")]
        [MaxLength(100, ErrorMessage = "O título do livro deve contér no máximo 100 caractéres!")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "O autor do livro deve ser referenciado!")]
        public AutorModel Autor { get; set; }
        [Required(ErrorMessage = "A categoria do livro deve ser informada obrigatóriamente!")]
        public CategoriaModel Categoria { get; set; }
        [Required(ErrorMessage = "A data de lançamento do livro deve ser informada obrigatóriamente!")]
        public DateTime DataDeLancamento { get; set; }
    }
}
