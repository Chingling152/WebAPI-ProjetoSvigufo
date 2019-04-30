using System;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase{

        IEventosRepository Repository;

        public EventosController() {
            Repository = new EventosRepository();
        }

        [HttpGet("listar/todos")]
        public IActionResult ListarEventos() {
            try {
                return Ok(Repository.Listar());
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("listar/{id}")]
        public IActionResult ListarEventos(int id) {
            try {
                return Ok(Repository.Listar(id));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("listar/tipo")]
        public IActionResult ListaTipo(TiposEventosDomain tipoEvento) {
            try {
                return Ok(Repository.Listar(tipoEvento));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet("listar/instituicao")]
        public IActionResult ListarInstituicao(InstituicoesViewModel instituicao) {
            try {
                return Ok(Repository.Listar(instituicao));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpGet("listar/data/{dataInicial}/{dataFinal}")]
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
                if (evento.Situacao.Equals(EnSituacaoEvento.Terminado) || evento.Situacao.Equals(EnSituacaoEvento.Cancelado)) {
                    throw new Exception("Você não pode cadastrar um evento cancelado ou que ja terminou");
                }
                if(evento.DataEvento < DateTime.Now) {
                    throw new Exception("A data do evento não pode ser menor do que o dia atual");
                }
                Repository.Cadastrar(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut("alterar")]
        public IActionResult AlterarEvento(EventosDomain evento) {
            try {
                if(evento.Situacao.Equals(EnSituacaoEvento.Terminado)) {
                    throw new Exception("Você não pode alterar um evento que ja ocorreu");
                }

                Repository.Alterar(evento);
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }
}
