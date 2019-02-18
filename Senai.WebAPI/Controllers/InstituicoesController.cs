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
        #region Get
        [HttpGet]
        public IActionResult ListarInstituicoes() {
            return Ok(Instituicao.Listar());
        }
        
        [HttpGet("{ID}")]
        public IActionResult BuscarPorID(int ID) {
            InstituicoesDomain valor = Instituicao.BuscarPorId(ID);
            if (valor == null){
                return NotFound("Instituição não encontrada");
            }
            return Ok(valor);
        }
        #endregion
        [HttpPost]
        public IActionResult InserirInstituicao(InstituicoesDomain instituicao) {
            try {
                Instituicao.Inserir(instituicao);
            } catch (Exception exc){
                return BadRequest(exc.Message);
            }

            return Ok(Instituicao.Listar());
        }

        [HttpPut]
        public IActionResult AtualizarInstituicao(InstituicoesDomain instituicao) {
            Instituicao.Editar(instituicao);
            return Ok(Instituicao.Listar());
        }

        [HttpDelete("{ID}")]
        public IActionResult RemoverInstituicao(int ID) {
            try {
                Instituicao.Deletar(ID);
                return Ok(Instituicao.Listar());
            } catch (Exception exc){
                return BadRequest(exc.Message);
            }
            
        }
    }
}