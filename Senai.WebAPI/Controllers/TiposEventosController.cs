using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;

namespace Senai.WebAPI.Controllers
{
    [Produces("application/json")]//Define a saida como JSON 
    [Route("api/[controller]")]//local onde sera encontrado a api
    [ApiController]//indica que a classe abaixo é uma API
    public class TiposEventosController : ControllerBase
    {

        private ITiposEventosRepository tiposEventos;

        public TiposEventosController()
        {
            tiposEventos = new TiposEventosRepository();
        }

        [HttpGet] //o nome do metodo não importa , pois o que define se ele é get ou set ou outra coisa é o [Http] acima dele
        public IEnumerable<TiposEventosDomain> RetornarView(){
            return tiposEventos.Listar();
        }

        [HttpPost]
        public IActionResult CadastrarTipoEvento(TiposEventosDomain tipoevento) 
        {
            try {
                tiposEventos.Cadastrar(tipoevento);
                return Ok(tiposEventos.Listar());
            } catch (Exception exc){
                return BadRequest("Não foi possivel cadastrar o Tipo de Evento\n"+exc.Message);
            }
        }
        
        [HttpPut]
        public IActionResult AtualizarObjeto(TiposEventosDomain EventoAtualizado) {
            try {
                tiposEventos.Alterar(EventoAtualizado);
                return Ok(tiposEventos.Listar());
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        
        [HttpDelete("{ID}")]
        public IActionResult RemoveObect(int ID)
        {
            try {
                tiposEventos.Deletar(ID);
                return Ok(tiposEventos.Listar());
            }catch(Exception exc) {
                return BadRequest(exc.Message);
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