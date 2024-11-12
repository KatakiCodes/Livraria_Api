using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LivrariaAPI.Models
{
    public class PerfilModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do autor deve ser informado obrigatóriamente!")]
        [MaxLength(30, ErrorMessage = "O primeiro nome do autor deve contér no máximo 30 caractéres!")]
        [MinLength(3, ErrorMessage = "O primeiro nome do autor deve contér no mínimo 3 caractéres!")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O sobrenome do autor deve ser informado obrigatóriamente!")]
        [MaxLength(30, ErrorMessage = "O sobrenome nome do autor deve contér no máximo 30 caractéres!")]
        [MinLength(3, ErrorMessage = "O sobrenome nome do autor deve contér no mínimo 3 caractéres!")]
        public string SobreNome { get; set; } = string.Empty;
        public DateTime DataDeNascimento { get; set; }
    }
}
