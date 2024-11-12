using LivrariaAPI.Dto.User;
using LivrariaAPI.Models;

namespace LivrariaAPI.Services.User
{
    public interface IUserService
    {
        public Task<ResponseModel<List<UserModel>>> ListarUtilizadores();
        public Task<ResponseModel<UserModel>> ListarUtilizadorePorId(int id);
        public Task<ResponseModel<UserModel>> CriarUtilizador(UserModel userModel);
        public Task<ResponseModel<List<UserModel>>> EditarUtilizador(int id, UserModel userModel);
        public Task<ResponseModel<List<UserModel>>> RemoverUtilizador(int id);
        public Task<ResponseModel<string>> Autenticar(AutenticacaoDto authDto);
    }
}
