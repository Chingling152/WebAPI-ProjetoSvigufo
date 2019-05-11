using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("v2/api/[controller]")]
    [ApiController]
    public class ConvitesController : ControllerBase {
        public readonly IConvitesRepository repositorio;

        public ConvitesController() {
            repositorio = new ConvitesRepository();
        }

        [Authorize]
        [HttpGet("listar/{pagina}/{quantidade}")]
        public IActionResult VerConvites(int pagina = 0, int quantidade = 10) {
            try {
                pagina = pagina < 0 ? 0 : pagina;
                quantidade = quantidade < 2 ? 2 : quantidade;
                return Ok(repositorio.Listar(pagina, quantidade));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/todos")]
        public IActionResult VerTodosConvites(){
            try {	        
		        return Ok(repositorio.Listar());
	        }
	        catch (Exception exc){
		        return BadRequest(new{erro = exc.Message});
	        }
        }
   

        [HttpGet("listar/convidados/{id}/todos")]
        public IActionResult VerTodosConvidados(int id) {
            try {
                return Ok(repositorio.ListarConvidados(id));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/convidados/{id}/{pagina}/{quantidade}")]
        public IActionResult VerConvidados(int id,int pagina = 0, int quantidade = 10) {
            try {
                pagina = pagina < 0 ? 0 : pagina;
                quantidade = quantidade < 2 ? 2 : quantidade;
                return Ok(repositorio.ListarConvidados(id,pagina, quantidade));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/{id}")]
        public IActionResult VerConvite(int id) {
            try {
                return Ok(repositorio.Listar(id));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }
        

        [HttpGet("listar/meusconvites/{pagina}/{quantidade}")]
        public IActionResult MeusConvites(int pagina = 0, int quantidade = 10) {
            try {
                pagina = pagina < 0 ? 0 : pagina;
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value
                );
                quantidade = quantidade < 2 ? 2 : quantidade;
                return Ok(repositorio.MeusConvites(ID,pagina,quantidade));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpGet("listar/meusconvites/{pagina}/{quantidade}/{situacao}")]
        public IActionResult MeusConvitesSituacao(int pagina = 0, int quantidade = 10,EnSituacaoConvite situacao = EnSituacaoConvite.Aguardando) {
            try {
                pagina = pagina < 0 ? 0 : pagina;
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value
                );
                quantidade = quantidade < 2 ? 2 : quantidade;
                return Ok(repositorio.MeusConvites(ID, pagina, quantidade,situacao));
            } catch (Exception exc) {
                return BadRequest(new { erro = exc.Message });
            }
        }

        [HttpGet("listar/minhaspalestras/{pagina}/{quantidade}")]
        public IActionResult MinhasPalestras(int pagina = 0, int quantidade = 10) {
            try {
                pagina = pagina < 0 ? 0 : pagina;
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(i => i.Type == JwtRegisteredClaimNames.Jti).Value
                );
                quantidade = quantidade < 2 ? 2 : quantidade;
                return Ok(repositorio.MinhasPalestras(ID, pagina, quantidade));
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpPost]
        [Route("inscricao")]
        public IActionResult SeInscrever(ConvitesDomain convite) {
            try {
                convite.IDUsuario = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                repositorio.Cadastrar(convite);
                return Ok("Você se inscreveu nesse evento com sucesso!");
            }catch(Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpPost]
        [Route("convidar")]
        public IActionResult Convidar(ConvitesDomain convite) {
            try {
                new UsuariosRepository().Listar(convite.IDUsuario);
                new EventosRepository().Listar(convite.ID);

                repositorio.Cadastrar(convite);
                return Ok("Convite enviado com sucesso!");
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

        [HttpPut("alterar")]
        public IActionResult AlterarStatus(ConvitesDomain convite) {
            try {
                repositorio.Alterar(convite);
                return Ok("Convite alterado com sucesso com sucesso!");
            } catch (Exception exc) {
                return BadRequest(new{erro = exc.Message});
            }
        }

    }
}
