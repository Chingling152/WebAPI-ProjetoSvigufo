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
        
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult ListarInstituicoes() {
            return Ok(Instituicao.Listar());
        }
        
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult InserirInstituicao(InstituicoesDomain instituicao) {
            try {
                Instituicao.Inserir(instituicao);
            } catch (Exception exc){
                return BadRequest(exc.Message);
            }

            return Ok(Instituicao.Listar());
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut]
        public IActionResult AtualizarInstituicao(InstituicoesDomain instituicao) {
            try{
                Instituicao.Editar(instituicao);
                return Ok(Instituicao.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }

}