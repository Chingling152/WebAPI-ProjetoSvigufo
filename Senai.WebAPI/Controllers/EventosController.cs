using System;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase{

        IEventosRepository Repository;

        public EventosController() {
            Repository = new EventosRepository();
        }

        [HttpGet]
        public IActionResult ListarEventos() {
            try {
                return Ok(Repository.Listar());
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost]
        public IActionResult InserirEvento(EventosDomain evento) {
            try {
                Repository.Inserir(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
        public IActionResult AlterarEvento(EventosDomain evento) {
            try {
                Repository.Alterar(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }
}
