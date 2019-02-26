using System;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Senai.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository repositorio;

        public UsuariosController() {
            repositorio = new UsuariosRepository();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(UsuariosDomain usuario) {
            try {
                repositorio.Cadastrar(usuario);
                return Ok($"{usuario.Nome} , {usuario.Email} , {usuario.Senha}");
            } catch(Exception exc) {
                return BadRequest(exc.Message);
            }
        }
    }
}