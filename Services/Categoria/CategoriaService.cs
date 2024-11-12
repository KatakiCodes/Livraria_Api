using LivrariaAPI.Data;
using LivrariaAPI.Dto.Categoria;
using LivrariaAPI.Models;
using LivrariaAPI.Services.ValidationObject;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Services.Categoria
{
    public class CategoriaService : ICategoriaService
    {
        private AppDbContext _appDbContext;
        private DataValidation _dataValidation;

        public CategoriaService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._dataValidation = new DataValidation();
        }
        public async Task<ResponseModel<List<CategoriaModel>>> CriarCategoria(CriacaoCategoriaDto categoriaDto)
        {
            var Response = new ResponseModel<List<CategoriaModel>>();
            var categoria = new CategoriaModel { Title = categoriaDto.Titulo };
            if (!this._dataValidation.Validate(categoria))
            {
                Response.Message = this._dataValidation.messageResult;
                Response.State = false;
            }
            else
            {
                try
                {
                    await this._appDbContext.Categorias.AddAsync(categoria);
                    await this._appDbContext.SaveChangesAsync();
                    Response = await this.ListarCategoria();
                    Response.Message = "Categoria cadastrada com sucesso";
                }
                catch (Exception ex)
                {
                    Response.Message = "Falha ao cadastrar a categoria " + ex.Message;
                    Response.State = false;
                }
            }
            return Response;
        }

        public async Task<ResponseModel<List<CategoriaModel>>> EditarCategoria(int id, EdicaoCategoriaDto categoriaDto)
        {
            var Response = new ResponseModel<List<CategoriaModel>>();
            var categoria = await this.ListarCategoriaPorId(id);
            if (categoria.Dados == null)
            {
                Response.Message = "Categoria não localizada";
                Response.State = false;
            }
            else
            {
                categoria.Dados.Title = categoriaDto.Titulo;
                if (!this._dataValidation.Validate(categoria.Dados))
                {
                    Response.Message = this._dataValidation.messageResult;
                    Response.State = false;
                }
                else
                {
                    try
                    {
                        this._appDbContext.Entry<CategoriaModel>(categoria.Dados).State = EntityState.Modified;
                        await this._appDbContext.SaveChangesAsync();
                        Response = await this.ListarCategoria();
                        Response.Message = "Categoria atualizada com sucesso";
                    }
                    catch (Exception ex)
                    {
                        Response.Message = "Falha ao atualizar a categoria " + ex.Message;
                        Response.State = false;
                    }
                }
            }

            return Response;
        }

        public async Task<ResponseModel<List<CategoriaModel>>> ListarCategoria()
        {
            var Response = new ResponseModel<List<CategoriaModel>>();
            try
            {
                var categorias = await this._appDbContext.Categorias.ToListAsync();
                if (categorias == null)
                    Response.Message = "Nenhuma categoria cadastrada";
                else
                {
                    Response.Message = "Categoria localizada";
                    Response.Dados = categorias;
                }
            }
            catch (Exception ex)
            {
                Response.Message = "Falha ao obter a lista de categorias " + ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<CategoriaModel>> ListarCategoriaPorId(int id)
        {
            var Response = new ResponseModel<CategoriaModel>();
            try
            {
                var categoria = await this._appDbContext.Categorias.Where(categoria => categoria.Id == id)
                    .FirstOrDefaultAsync();
                if (categoria == null)
                {
                    Response.Message = "Categoria não localizada";
                    Response.State = false;
                }
                else
                {
                    Response.Message = "Categoria localizada";
                    Response.Dados = categoria;
                }
            }
            catch (Exception ex)
            {
                Response.Message = "Falha ao localizar a categoria " + ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<CategoriaModel>>> RemoverCategoria(int id)
        {
            var Response = new ResponseModel<List<CategoriaModel>>();
            var categoria = await this.ListarCategoriaPorId(id);
            if(categoria.Dados == null)
            {
                Response.Message = "Categoria não localizada";
                Response.State = false;
            }
            else
            {
                try
                {
                    this._appDbContext.Categorias.Remove(categoria.Dados);
                    await this._appDbContext.SaveChangesAsync();
                    Response = await this.ListarCategoria();
                    Response.Message = "Categoria removida com sucesso";
                }
                catch(Exception ex)
                {
                    Response.Message = "Falha ao remover a categoria " + ex.Message;
                }
            }
            return Response;
        }
    }
}
