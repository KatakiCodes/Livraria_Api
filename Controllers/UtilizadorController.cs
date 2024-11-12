using LivrariaAPI.Dto.User;
using LivrariaAPI.Models;
using LivrariaAPI.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>>ListarUtilizadores(IUserService IuserService)
        {
            var utilizadores = await IuserService.ListarUtilizadores();
            return Ok(utilizadores);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<UserModel>>> ListarUtilizadorPorId(int id, IUserService IuserService)
        {
            var utilizador = await IuserService.ListarUtilizadorePorId(id);
            return Ok(utilizador);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel<UserModel>>> CriarUtilizador(IUserService IuserService, UserModel userModel)
        {
            var utilizadores = await IuserService.CriarUtilizador(userModel);
            return Ok(utilizadores);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<UserModel>>> EditarUtilizador(int id,IUserService IuserService, UserModel userModel)
        {
            var utilizadores = await IuserService.EditarUtilizador(id,userModel);
            return Ok(utilizadores);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<UserModel>>> RemoverUtilizador(int id, IUserService IuserService)
        {
            var utilizadores = await IuserService.RemoverUtilizador(id);
            return Ok(utilizadores);
        }

        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel<string>>> Autenticar(IUserService IuserService, AutenticacaoDto authDto)
        {
            var token = await IuserService.Autenticar(authDto);
            return Ok(token);
        }
    }
}
