using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using System;

namespace Senai.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicoesController : ControllerBase
    {
        public readonly IInstituicoesRepository Instituicao;

        public InstituicoesController()
        {
            Instituicao = new InstituicoesRepository();
        }
        
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult ListarInstituicoes() {
            return Ok(Instituicao.Listar());
        }

        [Authorize]
        [HttpGet("{ID}")]
        public IActionResult BuscarPorID(int ID) {
            InstituicoesDomain valor = Instituicao.BuscarPorId(ID);
            if (valor == null){
                return NotFound("Instituição não encontrada");
            }
            return Ok(valor);
        }
        
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult InserirInstituicao(InstituicoesDomain instituicao) {
            try {
                Instituicao.Inserir(instituicao);
            } catch (Exception exc){
                return BadRequest(exc.Message);
            }

            return Ok(Instituicao.Listar());
        }

        [Authorize]
        [HttpPut]
        public IActionResult AtualizarInstituicao(InstituicoesDomain instituicao) {
            Instituicao.Editar(instituicao);
            return Ok(Instituicao.Listar());
        }

    }
}