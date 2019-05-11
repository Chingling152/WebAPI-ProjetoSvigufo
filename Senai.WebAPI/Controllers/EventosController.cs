using System;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("v2/api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase{

        IEventosRepository Repository;

        public EventosController() {
            Repository = new EventosRepository();
        }

        [HttpGet("listar/todos")]
        public IActionResult ListarEventos(int quantidade = 10, int pagina = 10) {
            try {
                quantidade = quantidade < 1 ? 2 : quantidade;
                pagina = pagina < 0 ? 0 : pagina;
                return Ok(Repository.Listar(quantidade,pagina));
            }catch(Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/{quantidade}/{pagina}")]
        public IActionResult Listar(int quantidade = 10, int pagina = 10) {
            try {
                return Ok(Repository.Listar());
            } catch (Exception exc) {
                return BadRequest(new { erro = exc.Message });
            }
        }

        [HttpGet("listar/{id}")]
        public IActionResult ListarEventos(int id) {
            try {
                return Ok(Repository.Listar(id));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/tipo/{tipoEvento}/{quantidade}/{pagina}")]
        public IActionResult ListarTipo(int tipoEvento, int quantidade = 10, int pagina = 10) {
            try {
                quantidade= quantidade < 1? 2: quantidade;
                pagina = pagina<0? 0 : pagina;
                return Ok(Repository.ListarTipo(tipoEvento,quantidade, pagina));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/instituicao/{instituicao}/{quantidade}/{pagina}")]
        public IActionResult ListarInstituicao(int instituicao, int quantidade = 10, int pagina = 10) {
            try {
                quantidade = quantidade < 1 ? 2 : quantidade;
                pagina = pagina < 0 ? 0 : pagina;
                return Ok(Repository.ListarInstituicao(instituicao, quantidade,pagina));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }


        [HttpGet("listar/data/{dataInicial}/{dataFinal}/{quantidade}/{pagina}")]
        public IActionResult ListarEventos(DateTime dataInicial,DateTime dataFinal, int quantidade = 10, int pagina = 10) {
            try {
                quantidade = quantidade < 1 ? 2 : quantidade;
                pagina = pagina < 0 ? 0 : pagina;
                if (dataFinal < dataInicial) {
                    throw new Exception("A Data inicial não pode ser maior do que a data final");
                }
                return Ok(Repository.Listar(dataInicial,dataFinal, quantidade, pagina));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
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
                return BadRequest(new{erro = exc.Message});
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
                return BadRequest(new{erro = exc.Message});
            }
        }

    }
}
