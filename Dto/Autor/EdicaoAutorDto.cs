namespace LivrariaAPI.Dto.Autor
{
    public class EdicaoAutorDto
    {
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public DateTime DataDeNascimento { get; set; } = new DateTime();
    }
}
