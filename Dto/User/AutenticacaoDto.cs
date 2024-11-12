using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Dto.User
{
    public class AutenticacaoDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string palavraPasse { get; set; }
    }
}
