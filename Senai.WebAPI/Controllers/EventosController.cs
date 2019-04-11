using System;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
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

        [HttpGet("listar")]
        public IActionResult ListarEventos() {
            try {
                return Ok(Repository.Listar());
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("listar{id}")]
        public IActionResult ListarEventos(int id) {
            try {
                return Ok(Repository.Listar(id));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("listar")]
        public IActionResult ListarEventos(TiposEventosDomain tipoEvento) {
            try {
                return Ok(Repository.Listar(tipoEvento));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("listar{dataInicial}{dataFinal}")]
        public IActionResult ListarEventos(DateTime dataInicial,DateTime dataFinal) {
            try {
                if(dataFinal < dataInicial) {
                    throw new Exception("A Data inicial não pode ser maior do que a data final");
                }
                return Ok(Repository.Listar(dataInicial,dataFinal));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("cadastrar")]
        public IActionResult InserirEvento(EventosDomain evento) {
            try {
                Repository.Cadastrar(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut("alterar")]
        public IActionResult AlterarEvento(EventosDomain evento) {
            try {
                if(evento.ID == 0) {
                    throw new Exception("Você precisa especificar qual evento deseja alterar");
                }

                if(evento.Situacao.Equals(EnSituacaoEvento.Terminado) || evento.Situacao.Equals(EnSituacaoEvento.Cancelado)) {
                    throw new Exception("Você não pode alterar um evento que ja ocorreu ou foi cancelado");
                }

                Repository.Alterar(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }
}
