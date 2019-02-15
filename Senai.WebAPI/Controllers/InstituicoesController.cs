using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;

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
            return Ok(Instituicao.Listar());
        }
        
        [HttpGet("{ID}")]
        public IActionResult BuscarPorID(int ID) {
            return Ok(Instituicao.BuscarPorId(ID));
        }

        [HttpPost]
        public IActionResult InserirInstituicao(InstituicaoDomain instituicao) {
            Instituicao.Inserir(instituicao);
            return Ok(Instituicao.Listar());
        }

        [HttpPut]
        public IActionResult AtualizarInstituicao(InstituicaoDomain instituicao) {
            Instituicao.Editar(instituicao);
            return Ok(Instituicao.Listar());
        }

        [HttpDelete("{ID}")]
        public IActionResult RemoverInstituicao(int ID) {
            Instituicao.Deletar(ID);
            return Ok(Instituicao.Listar());
        }
    }
}