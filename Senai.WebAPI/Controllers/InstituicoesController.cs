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
        
        [HttpGet]
        public IActionResult ListarInstituicoes() {
            try { 
                return Ok(Instituicao.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult BuscarPorID(int ID) {
            try{
                return Ok(Instituicao.Listar(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
        
        [HttpPost]
        public IActionResult InserirInstituicao(InstituicoesDomain instituicao) {
            try {
                Instituicao.Cadastrar(instituicao);
                return Ok("Instituição cadastrada com sucesso");
            } catch (Exception exc){
                return BadRequest(exc.Message);
            }

        }
        [HttpPut]
        public IActionResult AtualizarInstituicao(InstituicoesDomain instituicao) {
            try {
                Instituicao.Atualizar(instituicao);
                return Ok("Instituição atualizada com sucesso");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
            
        }

    }
}