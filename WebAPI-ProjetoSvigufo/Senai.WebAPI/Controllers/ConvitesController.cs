using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConvitesController : ControllerBase {
        public readonly IConvitesRepository repositorio;

        public ConvitesController() {
            repositorio = new ConvitesRepository();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult VerConvites(){
            try {	        
		        return Ok(repositorio.Listar());
	        }
	        catch (Exception exc){
		        return BadRequest(exc.Message);
	        }
        }

        [Authorize]
        [HttpGet]
        [Route("Meus")]//muda o caminho do site
        public IActionResult VerMeusConvites() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(repositorio.ListarMeusConvites(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpPost("entrar/{eventoID}")]
        public IActionResult SeInscrever(int eventoID) {
            try {
                ConvitesDomain convite = new ConvitesDomain() {
                    Evento = new EventosViewModel() {
                        ID = eventoID
                    },
                    Usuario = new UsuariosViewModel() {
                        ID = Convert.ToInt32(
                            HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                        )
                    },
                    Status = Enums.EnSituacaoConvite.Aguardando
                };
                repositorio.Cadastrar(convite);
                return Ok("Convite enviado com sucesso!");
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpPost("convidar")]
        public IActionResult Convidar(ConvitesDomain convite) {
            try {
                repositorio.Cadastrar(convite);
                return Ok("Convite enviado com sucesso!");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [Authorize]
        [HttpPut("alterar")]
        public IActionResult AlterarStatus(ConvitesDomain convite) {
            try {
                repositorio.Alterar(convite);
                return Ok("Convite alterado com sucesso com sucesso!");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

    }
}
