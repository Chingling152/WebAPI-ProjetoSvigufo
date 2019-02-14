using Microsoft.AspNetCore.Mvc;

using System.Linq;
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

        [HttpGet] //o nome do metodo nçao importa , pois o que definse se ele é get ou set ou outra coisa é o [Http] acima dele
        public IEnumerable<TiposEventosDomain> RetornarView(){
            return tiposEventos.Listar();
        }

        [HttpGet("{ID}")]//Define o parametro na URL
        public IActionResult BuscarPorID(int ID) {
            return Ok(tiposEventos.ListarPorID(ID));
        }

        [HttpPost]
        public IActionResult CadastrarTipoEvento(TiposEventosDomain tipoevento)//from body 
        {
            tiposEventos.Cadastrar(tipoevento);
            return Ok(tiposEventos.Listar());
        }
        
        [HttpPut]
        public IActionResult AtualizarObjeto(TiposEventosDomain EventoAtualizado) {
            tiposEventos.Alterar(EventoAtualizado);
            return Ok(tiposEventos.Listar());
        }

        
        [HttpDelete("{ID}")]
        public IActionResult RemoveObect(int ID)
        {
            tiposEventos.Deletar(ID);
            return Ok(tiposEventos.Listar());
        }
        /*
        *  GET     = Buscar
        *  POST    = Enviar
        *  PUT     = Atualizar
        *  DELETE  = Deletar
        */
    }
}