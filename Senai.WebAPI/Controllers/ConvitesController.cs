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

        [HttpGet]
        public IActionResult VerConvites(){
            try {	        
		        return Ok(repositorio.Listar());
	        }
	        catch (Exception exc){
		        return BadRequest(exc.Message);
	        }
        }

        [HttpGet]
        [Route("meus")]//muda o caminho do site
        public IActionResult VerMeusConvites() {
            try {
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(repositorio.MeusConvites(ID));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("se_inscrever")]
        public IActionResult SeInscrever(ConvitesDomain convite) {
            try {
                convite.IDUsuario = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                repositorio.Cadastrar(convite);
                return Ok("Você se inscreveu nesse evento com sucesso!");
            }catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPost("convidar")]
        public IActionResult Convidar(ConvitesDomain convite) {
            try {
                repositorio.Cadastrar(convite);
                return Ok("Convite enviado com sucesso!");
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }

        [HttpPut]
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
