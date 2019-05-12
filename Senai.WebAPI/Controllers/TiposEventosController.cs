using System;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;

namespace Senai.WebAPI.Controllers
{
    [Produces("application/json")]//Define a saida como JSON 
    [Route("api/v2/[controller]")]//local onde sera encontrado a api
    [ApiController]//indica que a classe abaixo é uma API
    public class TiposEventosController : ControllerBase
    {

        private ITiposEventosRepository tiposEventos;

        public TiposEventosController()
        {
            tiposEventos = new TiposEventosRepository();
        }

        [Authorize(Roles = "Administrador,Organizador")]
        [HttpGet("listar")] //o nome do metodo não importa , pois o que define se ele é get ou set ou outra coisa é o [Http] acima dele
        public IActionResult Listar() {
            try {
                return Ok(tiposEventos.Listar());
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(TiposEventosDomain tipoevento) 
        {
            try {
                tiposEventos.Cadastrar(tipoevento);
                return Ok($"O Tipo de evento {tipoevento.Nome} foi cadastrado com sucesso!");
            } catch (SqlException exc) {
                return BadRequest("Ocorreu um problema ao cadastrar o banco de dados\n" + exc.Message);
            } catch (Exception exc){
                return BadRequest("Não foi possivel cadastrar o Tipo de Evento\n"+exc.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("alterar")]
        public IActionResult Atualizar(TiposEventosDomain EventoAtualizado) {
            try {
                if(EventoAtualizado.ID == 0)
                    throw new Exception("Você precisa especificar o ID do tipo de evento");

                tiposEventos.Alterar(EventoAtualizado);
                return Ok(tiposEventos.Listar());
            } catch (SqlException exc) {
                return BadRequest("Ocorreu um problema com o banco de dados\n"+ exc);
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            } 
        }

        /*
        *  GET     = Buscar
        *  POST    = Enviar
        *  PUT     = Atualizar
        *  DELETE  = Deletar
        */
    }
}