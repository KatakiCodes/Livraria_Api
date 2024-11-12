using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do utilizador deve ser informado obrigatóriamente")]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A palavra passe do utilizador deve ser informado obrigatóriamente")]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required(ErrorMessage = "O tipo de utilizador deve ser referênciado")]
        public string Role { get; set; }
    }
}
