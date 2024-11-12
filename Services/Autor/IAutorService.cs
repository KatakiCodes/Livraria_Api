using LivrariaAPI.Dto.Autor;
using LivrariaAPI.Models;

namespace LivrariaAPI.Services.Autor
{
    public interface IAutorService
    {
        public Task<ResponseModel<List<AutorModel>>> ListarAutores();
        public Task<ResponseModel<AutorModel>> ListarAutorPorId(int id);
        public Task<ResponseModel<AutorModel>> ListarAutorPorLivro(int idLivro);
        public Task<ResponseModel<List<AutorModel>>> CriarAutor(CriacaoAutorDto autorDto);
        public Task<ResponseModel<List<AutorModel>>> EditarAutor(int id,EdicaoAutorDto autorDto);
        public Task<ResponseModel<List<AutorModel>>> RemoverAutor(int id);
    }
}
