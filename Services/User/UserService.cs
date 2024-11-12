using LivrariaAPI.Data;
using LivrariaAPI.Dto.User;
using LivrariaAPI.Models;
using LivrariaAPI.Services.ValidationObject;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Services.User
{
    public class UserService : IUserService
    {
        private AppDbContext _appDbContext;
        private DataValidation _dataValidation;

        public UserService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._dataValidation = new DataValidation();
        }

        public async Task<ResponseModel<string>> Autenticar(AutenticacaoDto authDto)
        {
            var Response = new ResponseModel<string>();
            if (!this._dataValidation.Validate(authDto))
            {
                Response.Message = this._dataValidation.messageResult;
                Response.State = false;
            }

            else
            {
                var user = await this._appDbContext.Utilizadores
                    .Where(x => x.Nome == authDto.Nome && x.Password == authDto.palavraPasse)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    Response.Message = "Nome ou palavra passe inválida";
                    Response.State = false;
                }
                else
                    Response.Dados = TokenService.GerarToken(user);
            }
                return Response;
        }

        public async Task<ResponseModel<UserModel>> CriarUtilizador(UserModel userModel)
        {
            var Response = new ResponseModel<UserModel>();

            if (!this._dataValidation.Validate(userModel))
            {
                Response.Message = this._dataValidation.messageResult;
                Response.State = false;
            }
            else
            {
                try
                {
                    await this._appDbContext.Utilizadores.AddAsync(userModel);
                    await this._appDbContext.SaveChangesAsync();
                    Response.Dados = new UserModel {Nome = userModel.Nome,Role = userModel.Role};
                    Response.Message = "Utilizador cadastrado com sucesso!";
                }
                catch (Exception ex)
                {
                    Response.Message = $"Falha ao cadastrar o utilizador {ex.Message}";
                }
            }
            return Response;

        }

        public async Task<ResponseModel<List<UserModel>>> EditarUtilizador(int id, UserModel userModel)
        {
            var Response = new ResponseModel<List<UserModel>>();

            var utilizador = await this.ListarUtilizadorePorId(id);
            if (utilizador.Dados == null)
            {
                Response.Message = "Não foi possível localizar o utilizador";
                Response.State = false;
            }
            else
            {
                utilizador.Dados.Nome = userModel.Nome;
                utilizador.Dados.Password = userModel.Password;
                utilizador.Dados.Role = userModel.Role;

                if (!this._dataValidation.Validate(utilizador.Dados))
                {
                    Response.Message = this._dataValidation.messageResult;
                    Response.State = false;
                }
                else
                {
                    try
                    {
                        this._appDbContext.Entry<UserModel>(utilizador.Dados).State = EntityState.Modified;
                        await this._appDbContext.SaveChangesAsync();
                        Response = await this.ListarUtilizadores();
                        Response.Message = "Utilizador atualizado com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        Response.Message = $"Falha ao atualizar o utilizador: {ex.Message}";
                        Response.State = false;
                    }
                }
            }
            return Response;
        }

        public async Task<ResponseModel<UserModel>> ListarUtilizadorePorId(int id)
        {
            var Response = new ResponseModel<UserModel>();
            try
            {
                var utilizadore = await this._appDbContext.Utilizadores.
                    Where(utilizadorDb => utilizadorDb.Id == id).FirstOrDefaultAsync();
                if (utilizadore == null)
                    Response.Message = "Utilizador não localizado";
                else
                    Response.Message = "Utilizador localizado com sucesso!";
                Response.Dados = utilizadore;
            }
            catch (Exception ex)
            {
                Response.Message = $"Falha ao localizar o utilizador: {ex.Message}";
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<UserModel>>> ListarUtilizadores()
        {

            var Response = new ResponseModel<List<UserModel>>();
            try
            {
                var utilizadores = await this._appDbContext.Utilizadores.ToListAsync();
                if (utilizadores == null)
                    Response.Message = "Nenhum utilizador cadastrado";
                else
                    Response.Message = "Lista de utilizadore obtida com sucesso!";
                Response.Dados = utilizadores;
            }
            catch (Exception ex)
            {
                Response.Message = $"Falha ao obter a lista de utilizadores: {ex.Message}";
                Response.State = false;
            }
            return Response;
        }

        public async Task<ResponseModel<List<UserModel>>> RemoverUtilizador(int id)
        {
            var Response = new ResponseModel<List<UserModel>>();

            var utilizador = await this.ListarUtilizadorePorId(id);
            if (utilizador.Dados == null)
            {
                Response.Message = "Não foi possível localizar o utilizador";
                Response.State = false;
            }
            else
            {
                try
                {
                    this._appDbContext.Utilizadores.Remove(utilizador.Dados);
                    await this._appDbContext.SaveChangesAsync();
                    Response = await this.ListarUtilizadores();
                    Response.Message = "Utilizador removida com sucesso!";
                }
                catch (Exception ex)
                {
                    Response.Message = $"Falha ao remover o utilizador: {ex.Message}";
                    Response.State = false;
                }
            }
            return Response;
        }
    }
}
