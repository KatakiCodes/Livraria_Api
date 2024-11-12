using LivrariaAPI.Data;
using LivrariaAPI.Dto.Livro;
using LivrariaAPI.Models;
using LivrariaAPI.Services.ValidationObject;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Services.Livro
{
    public class LivroService : ILivroService
    {
        private AppDbContext _appDbContext;
        private DataValidation _dataValidation;

        public LivroService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._dataValidation = new DataValidation();
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(CriacaoLivroDto livroDto)
        {
            var Response = new ResponseModel<List<LivroModel>>();
            var GetAutor = await this._appDbContext.Autores.Where(x => x.Id == livroDto.IdAutor)
                .FirstOrDefaultAsync();
            var GetCategoria = await this._appDbContext.Categorias.Where(x => x.Id == livroDto.IdCategoria)
                .FirstOrDefaultAsync();
            if (GetCategoria == null)
            {
                Response.Message = "Categoria não localizada";
                Response.State = false;
            }
            else if (GetAutor == null)
            {
                Response.Message = "Autor não localizado";
                Response.State = false;
            }
            else
            {
                var livroModel = new LivroModel
                {
                    Title = livroDto.Titulo,
                    Categoria = GetCategoria,
                    Autor = GetAutor,
                    DataDeLancamento = livroDto.DateDeLancamento
                };
                if (!this._dataValidation.Validate(livroModel))
                {
                    Response.Message = this._dataValidation.messageResult;
                    Response.State = false;
                }
                else
                {
                    try
                    {
                        await this._appDbContext.Livros.AddAsync(livroModel);
                        await this._appDbContext.SaveChangesAsync();

                        Response = await this.ListarLivros();
                        Response.Message = "Livro cadastrado com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        Response.Message = $"Falha ao cadastrar o livro {ex.Message}";
                        Response.State = false;
                    }
                }
            }


            return Response;
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(int id, EdicaoLivroDto livroDto)
        {
            var Response = new ResponseModel<List<LivroModel>>();
            var getLivro = await this.ListarLivroPorId(id);
            var livro = getLivro.Dados;
            if (livro == null)
            {
                Response.Message = "Livro não localizado!";
                Response.State = false;
            }
            else
            {
                var GetAutor = await this._appDbContext.Autores.Where(x => x.Id == livroDto.IdAutor)
               .FirstOrDefaultAsync();
                var GetCategoria = await this._appDbContext.Categorias.Where(x => x.Id == livroDto.IdCategoria)
                    .FirstOrDefaultAsync();
                if (GetCategoria == null)
                {
                    Response.Message = "Categoria não localizada";
                    Response.State = false;
                }
                else if (GetAutor == null)
                {
                    Response.Message = "Autor não localizado";
                    Response.State = false;
                }
                else 
                {
                    livro.Title = livroDto.Titulo;
                    livro.Categoria = GetCategoria;
                    livro.Autor = GetAutor;
                    livro.DataDeLancamento = livroDto.DateDeLancamento;

                    if (!this._dataValidation.Validate(livro))
                    {
                        Response.Message = this._dataValidation.messageResult;
                        Response.State = false;
                    }
                    else
                    {
                        try
                        {
                            this._appDbContext.Entry<LivroModel>(livro).State = EntityState.Modified;
                            await this._appDbContext.SaveChangesAsync();
                            Response = await this.ListarLivros();
                            Response.Message = "Livro atualizado com sucesso!";
                        }
                        catch (Exception ex)
                        {
                            Response.Message = $"Falha ao Atualizar o livro {ex.Message}";
                            Response.State = false;
                        }
                    }
                } 
            }

            return Response;
        }

        public async Task<ResponseModel<LivroModel>> ListarLivroPorId(int id)
        {
            ResponseModel<LivroModel> Response = new ResponseModel<LivroModel>();
            try
            {
                var Livro = await this._appDbContext.Livros
                    .Include(livro => livro.Autor)
                    .Include(livro => livro.Categoria)
                    .Where(livro => livro.Id == id).FirstOrDefaultAsync();
                Response.Dados = Livro;
                Response.Message = (Livro == null) ? "Nenhum livro não localizado" : "Livro localizado com sucesso!";
                Response.State = (Livro == null) ? false : true;
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> Response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await this._appDbContext.Livros
                    .Include(livro => livro.Autor)
                    .Include(livro => livro.Categoria).ToListAsync();
                Response.Dados = livros;
                Response.Message = (livros == null) ? "Nenhum livro cadastrado" : "Lista de livros obtida com sucesso!";
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<LivroModel>> ListarLivrosPorAutor(int idAutor)
        {
            ResponseModel<LivroModel> Response = new ResponseModel<LivroModel>();
            try
            {
                var Livro = await this._appDbContext.Livros.Include(livroDb => livroDb.Autor)
                    .Include(livro => livro.Categoria)
                    .Where(livrosDb => livrosDb.Id == idAutor).FirstOrDefaultAsync();
                if (Livro == null)
                {
                    Response.Message = "Livro não localizado!";
                    Response.State = false;
                }
                else
                {
                    Response.Dados = Livro;
                    Response.Message = "Livro localizado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivrosPorCategoria(int idCategoria)
        {
            ResponseModel<List<LivroModel>> Response = new ResponseModel<List<LivroModel>>();
            try
            {
                var Livros = await this._appDbContext.Livros.Include(livroDb => livroDb.Autor)
                    .Include(livro => livro.Categoria)
                    .Where(livrosDb => livrosDb.Categoria.Id == idCategoria).ToListAsync();
                if (Livros == null)
                {
                    Response.Message = "Categoria não localizada!";
                    Response.State = false;
                }
                else
                {
                    Response.Dados = Livros;
                    Response.Message = "Categoria localizada com sucesso!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<LivroModel>>> RemoverLivro(int idLivro)
        {
            var Response = new ResponseModel<List<LivroModel>>();
            var getLivro = await this.ListarLivroPorId(idLivro);
            var livro = getLivro.Dados;
            if (livro == null)
            {
                Response.Message = "Livro não localizado";
                Response.State = false;
            }
            else
            {
                try
                {
                    this._appDbContext.Livros.Remove(livro);
                    await this._appDbContext.SaveChangesAsync();
                    Response = await this.ListarLivros();
                    Response.Message = "Livro removido com sucesso";
                }
                catch (Exception ex)
                {
                    Response.Message = "Ocorreu um erro ao remover o livro " + ex.Message;
                    Response.State = false;
                }
            }
            return Response;
        }
    }
}
