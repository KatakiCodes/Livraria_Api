using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace LivrariaAPI.Models
{
    public class AutorModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O perfil do autor deve ser referenciado obrigatoriamente")]
        public PerfilModel Perfil { get; set; }
        [JsonIgnore]
        public IEnumerable<LivroModel> Livros { get; set; }
    }
}
