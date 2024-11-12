using LivrariaAPI.Dto.Autor;
using LivrariaAPI.Models;
using LivrariaAPI.Services.Autor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("v1/autores")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> BuscarAutor(IAutorService IAutorService)
        {
            var Autores = await IAutorService.ListarAutores();
            return Ok(Autores);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> BuscarAutorPorId(int id, IAutorService IAutorService)
        {
            var Autor = await IAutorService.ListarAutorPorId(id);
            return Ok(Autor);
        }

        [HttpGet]
        [Route("/livros/{idLivro:int}")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> BuscarAutorPorLivro(int idLivro, IAutorService IAutorService)
        {
            var Autor = await IAutorService.ListarAutorPorLivro(idLivro);
            return Ok(Autor);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> CriarAutor(IAutorService IAutorService, CriacaoAutorDto autorDto)
        {
            var Autor = await IAutorService.CriarAutor(autorDto);
            return Ok(Autor);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> EditarAutor(IAutorService IAutorService,int id, EdicaoAutorDto autorDto)
        {
            var Autor = await IAutorService.EditarAutor(id,autorDto);
            return Ok(Autor);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> RemoverAutor(IAutorService IAutorService, int id)
        {
            var Autor = await IAutorService.RemoverAutor(id);
            return Ok(Autor);
        }
    }
}
