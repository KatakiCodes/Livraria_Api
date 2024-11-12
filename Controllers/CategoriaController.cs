using LivrariaAPI.Dto.Categoria;
using LivrariaAPI.Models;
using LivrariaAPI.Services.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("v1/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>>ListarCategoria(ICategoriaService IcategoriaService)
        {
            var categorias = await IcategoriaService.ListarCategoria();
            return Ok(categorias);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> ListarCategoriaPorId(int id, ICategoriaService IcategoriaService)
        {
            var categoria = await IcategoriaService.ListarCategoriaPorId(id);
            return Ok(categoria);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> CriarCategoria(ICategoriaService IcategoriaService, CriacaoCategoriaDto categoriaDto)
        {
            var categorias = await IcategoriaService.CriarCategoria(categoriaDto);
            return Ok(categorias);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> EditarCategoria(int id, ICategoriaService IcategoriaService, EdicaoCategoriaDto categoriaDto)
        {
            var categorias = await IcategoriaService.EditarCategoria(id,categoriaDto);
            return Ok(categorias);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> RemoverCategoria(int id, ICategoriaService IcategoriaService)
        {
            var categorias = await IcategoriaService.RemoverCategoria(id);
            return Ok(categorias);
        }
    }
}
