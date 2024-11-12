using LivrariaAPI.Dto.Autor;
using LivrariaAPI.Dto.Livro;
using LivrariaAPI.Models;
using LivrariaAPI.Services.Autor;
using LivrariaAPI.Services.Livro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("v1/livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>>ListarLivros(ILivroService IlivroService)
        {
            var livros = await IlivroService.ListarLivros();
            return Ok(livros);
        }

        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> ListarLivrosPorId(int id, ILivroService IlivroService)
        {
            var livros = await IlivroService.ListarLivroPorId(id);
            return Ok(livros);
        }

        [HttpGet]
        [Authorize]
        [Route("/categoria/{idCategoria:int}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivrosPorCategoria(int idCategoria, ILivroService IlivroService)
        {
            var livros = await IlivroService.ListarLivrosPorCategoria(idCategoria);
            return Ok(livros);
        }

        [HttpGet]
        [Authorize]
        [Route("/autor/{idAutor:int}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> ListarLivrosPorAutor(int idAutor, ILivroService IlivroService)
        {
            var livros = await IlivroService.ListarLivrosPorCategoria(idAutor);
            return Ok(livros);
        }


        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CriarLivro(ILivroService ILivroService, CriacaoLivroDto livroDto)
        {
            var livros = await ILivroService.CriarLivro(livroDto);
            return Ok(livros);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(ILivroService ILivroService, int id, EdicaoLivroDto livroDto)
        {
            var livros = await ILivroService.EditarLivro(id, livroDto);
            return Ok(livros);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> RemoverLivro(ILivroService ILivroService, int id)
        {
            var livros = await ILivroService.RemoverLivro(id);
            return Ok(livros);
        }
    }
}

