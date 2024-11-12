using Azure;
using LivrariaAPI.Data;
using LivrariaAPI.Dto.Autor;
using LivrariaAPI.Models;
using LivrariaAPI.Services.ValidationObject;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Services.Autor
{
    public class AutorService : IAutorService
    {
        private readonly AppDbContext _appDbContext;
        private DataValidation _dataValidation;
        public AutorService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._dataValidation = new DataValidation();
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> Response = new ResponseModel<List<AutorModel>>();
            try
            {
                var Autores = await this._appDbContext.Autores
                    .Include(autorDb => autorDb.Perfil).ToListAsync();
                Response.Dados = Autores;
                Response.Message = (Autores == null) ? "Nenhum autor cadastrado" : "Lista de autores obtida com sucesso!";
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<AutorModel>> ListarAutorPorId(int id)
        {
            ResponseModel<AutorModel> Response = new ResponseModel<AutorModel>();
            try
            {
                var Autor = await this._appDbContext.Autores
                    .Include(autorDb => autorDb.Perfil)
                    .Where(autorDb => autorDb.Id == id).FirstOrDefaultAsync();
                Response.Dados = Autor;
                Response.Message = (Autor == null) ? "Autor não localizado" : "Autor localizado com sucesso!";
                Response.State = (Autor == null) ? false : true;
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<AutorModel>> ListarAutorPorLivro(int idLivro)
        {
            ResponseModel<AutorModel> Response = new ResponseModel<AutorModel>();
            try
            {
                var Livro = await this._appDbContext.Livros.Include(livroDb => livroDb.Autor)
                    .Where(livrosDb => livrosDb.Id == idLivro).FirstOrDefaultAsync();
                if (Livro == null)
                {
                    Response.Message = "Livro não localizado!";
                    Response.State = false;
                }
                else
                {
                    var Autor = await this._appDbContext.Autores
                        .Include(autorDb => autorDb.Perfil)
                        .Where(autorDb => autorDb.Id == Livro.Autor.Id).FirstOrDefaultAsync();
                    Response.Dados = Autor;
                    Response.Message = "Autor localizado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.State = false;
            }
            return Response;
        }


        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(CriacaoAutorDto autorDto)
        {
            var Response = new ResponseModel<List<AutorModel>>();
            var autorModel = new AutorModel();

            var perfilModel = new PerfilModel
            {
                Nome = autorDto.Nome,
                SobreNome = autorDto.SobreNome,
                DataDeNascimento = autorDto.DataDeNascimento
            };
            if (!this._dataValidation.Validate(perfilModel))
            {
                Response.Message = this._dataValidation.messageResult;
                Response.State = false;
            }
            else
            {
                try
                {
                    await this._appDbContext.Perfis.AddAsync(perfilModel);
                    await this._appDbContext.SaveChangesAsync();
                    autorModel.Perfil = perfilModel;
                    await this._appDbContext.Autores.AddAsync(autorModel);
                    await this._appDbContext.SaveChangesAsync();

                    Response.Message = "Autor cadastrado com sucesso!";
                    Response.Dados = await this._appDbContext.Autores.Include(x => x.Perfil).ToListAsync();
                }
                catch (Exception ex)
                {
                    Response.Message = $"Falha ao cadastrar o autor {ex.Message}";
                    Response.State = false;
                }
            }
            return Response;
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(int id, EdicaoAutorDto autorDto)
        {
            var Response = new ResponseModel<List<AutorModel>>();
            var Getautor = await this.ListarAutorPorId(id);
            var autor = Getautor.Dados;
            if (autor == null)
            {
                Response.Message = "Autor não localizado";
                Response.State = false;
            }
            else
            {
                autor.Perfil.Nome = autorDto.Nome;
                autor.Perfil.SobreNome = autorDto.SobreNome;
                autor.Perfil.DataDeNascimento = autorDto.DataDeNascimento;

                if (!this._dataValidation.Validate(autor.Perfil))
                {
                    Response.Message = this._dataValidation.messageResult;
                    Response.State = false;
                }
                else
                {
                    try
                    {
                        this._appDbContext.Entry<PerfilModel>(autor.Perfil).State = EntityState.Modified;
                        await this._appDbContext.SaveChangesAsync();

                        Response = await this.ListarAutores();
                        Response.Message = "Autor removido com sucesso";
                    }
                    catch (Exception ex)
                    {
                        Response.Message = $"Falha ao cadastrar o autor {ex.Message}";
                        Response.State = false;
                    }
                }
            }
            return Response;
        }


        public async Task<ResponseModel<List<AutorModel>>> RemoverAutor(int id)
        {
            var response = new ResponseModel<List<AutorModel>>();
            var Autor = await this.ListarAutorPorId(id);
            if (Autor.Dados == null)
            {
                response.Message = "Autor não localizado!";
                response.State = false;
            }
            else
            {
                try
                {
                    this._appDbContext.Perfis.Remove(Autor.Dados.Perfil);
                    await this._appDbContext.SaveChangesAsync();
                    response = await this.ListarAutores();
                    response.Message = "Autor removido com sucesso";
                }
                catch (Exception ex)
                {
                    response.Message = "Ocorreu um erro ao remover o autor" + ex.Message;
                    response.State = false;
                }
            }
            return response;
        }
    }
}
