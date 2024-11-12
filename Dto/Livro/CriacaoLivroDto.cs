namespace LivrariaAPI.Dto.Livro
{
    public class CriacaoLivroDto
    {
        public int IdAutor { get; set; }
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
        public DateTime DateDeLancamento { get; set; }
    }
}
