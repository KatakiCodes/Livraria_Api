using LivrariaAPI.Dto.Livro;
using LivrariaAPI.Models;

namespace LivrariaAPI.Services.Livro
{
    public interface ILivroService
    {
        public Task<ResponseModel<List<LivroModel>>> ListarLivros();
        public Task<ResponseModel<LivroModel>> ListarLivroPorId(int id);
        public Task<ResponseModel<List<LivroModel>>> ListarLivrosPorCategoria(int idCategoria);
        public Task<ResponseModel<LivroModel>> ListarLivrosPorAutor(int idAutor);
        public Task<ResponseModel<List<LivroModel>>> CriarLivro(CriacaoLivroDto livroDto);
        public Task<ResponseModel<List<LivroModel>>> EditarLivro(int id, EdicaoLivroDto livroDto);
        public Task<ResponseModel<List<LivroModel>>> RemoverLivro(int idLivro);
    }
}
