using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Views;
using Senai.WebAPI.Repositorios;

namespace Senai.WebAPI.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private IUsuariosRepository repositorio;

        public LoginController() {
            repositorio = new UsuariosRepository();
        }

        [HttpPost]
        public IActionResult LogarUsuario(LoginViewModel login) {

            UsuariosDomain usuario = repositorio.BuscarPorEmailSenha(login.Email, login.Senha);

            try {
                if (usuario == null) {
                    return NotFound("Email ou senha incorretos");
                }

                var clains = new[] {
                    new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,usuario.ID.ToString()),
                    new Claim(ClaimTypes.Role,usuario.TipoUsuario.ToString())
                };

                var Chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SVIGUFO-CHAVE-AUTENTICACAO"));

                var Credencial = new SigningCredentials(Chave,SecurityAlgorithms.HmacSha384);

                var Token= new JwtSecurityToken(
                    issuer: "Svigufo.WebApi",
                    audience : "Svigufo.WebApi",
                    claims: clains,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials:Credencial
                    );

                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(Token)
                });
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}
