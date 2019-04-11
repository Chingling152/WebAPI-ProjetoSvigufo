using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("meusconvites{quantidade}")]//muda o caminho do site
        public IActionResult VerMeusConvites(int quantidade =10) {//valor padrão de busca
            try {
                quantidade = quantidade < 1 ? 1 : quantidade;
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(repositorio.MeusConvites(ID).Take(quantidade));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpGet]
        [Route("meusconvites/pendentes{quantidade}")]
        public IActionResult VerMeusConvitesPendentes(int quantidade = 10) {
            try {
                quantidade = quantidade < 1? 1:quantidade;
                int ID = Convert.ToInt32(
                    HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value
                );
                return Ok(repositorio.MeusConvites(ID).Where(i=> i.Status.Equals(EnSituacaoConvite.Aguardando)).Take(quantidade));
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }


        [HttpPost("seinscrever")]
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
