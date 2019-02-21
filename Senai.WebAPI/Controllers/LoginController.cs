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

                //cria claims que são
                Claim[] claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,usuario.ID.ToString()),
                    new Claim(ClaimTypes.Role,usuario.TipoUsuario.ToString())
                };

                //define a chave do toeken
                SymmetricSecurityKey Chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SVIGUFO-CHAVE-AUTENTICACAO"));

                //define o algoritmo de criptografia do token
                SigningCredentials Credencial = new SigningCredentials(Chave,SecurityAlgorithms.HmacSha384);


                JwtSecurityToken Token = new JwtSecurityToken(
                    issuer: "Svigufo.WebApi",//Usuario que manda a requisição
                    audience : "Usuario.Logado",//Usuario que recebe a requisição
                    claims: claims,//informações do usuario criptografadas
                    expires: DateTime.Now.AddMinutes(30),//seta um tempo de expiração de 30 minutos
                    signingCredentials:Credencial// utiliza a credencial
                    );

                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(Token)//cria o token
                });
            } catch (Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}
