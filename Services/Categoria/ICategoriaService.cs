using LivrariaAPI.Dto.Categoria;
using LivrariaAPI.Models;

namespace LivrariaAPI.Services.Categoria
{
    public interface ICategoriaService
    {
        public Task<ResponseModel<List<CategoriaModel>>> ListarCategoria();
        public Task<ResponseModel<CategoriaModel>> ListarCategoriaPorId(int id);
        public Task<ResponseModel<List<CategoriaModel>>> CriarCategoria(CriacaoCategoriaDto categoriaDto);
        public Task<ResponseModel<List<CategoriaModel>>> EditarCategoria(int id, EdicaoCategoriaDto categoriaDto);
        public Task<ResponseModel<List<CategoriaModel>>> RemoverCategoria(int id);
    }
}
