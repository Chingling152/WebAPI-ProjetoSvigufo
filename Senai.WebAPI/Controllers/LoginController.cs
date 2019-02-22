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

                var claims = new[] {
                    // claim : conjunto de chave e valor 
                    new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,usuario.ID.ToString()),
                    // new Claim(ClaimTypes.Role,usuario.TipoUsuario.ToString())
                };

                // Chave de acesso do token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("svigufo-chave-autenticacao"));

                //Credenciais do Token - Header
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Gera o token
                var token = new JwtSecurityToken(
                    issuer: "SviGufo.WebApi",
                    audience: "SviGufo.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)//cria o token
                });
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}
