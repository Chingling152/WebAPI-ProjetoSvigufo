using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    interface IUsuariosRepository {

        /// <summary>
        /// Mostra todos os usuarios cadastrados no banco de dados
        /// </summary>
        /// <returns>Todos os usuarios do banco de dados</returns>
        List<UsuariosDomain> Listar();

        /// <summary>
        /// Mostra um usuario no ID enviado por parametro
        /// </summary>
        /// <param name="ID">ID Do usuario que será retornado</param>
        /// <returns>Todas as informações do usuario</returns>
        UsuariosDomain Listar(int ID);

        /// <summary>
        /// Procura todos os usuarios 
        /// </summary>
        /// <param name="tipoUsuario">Tipo de Usuario usado na filtragem</param>
        /// <returns>Uma lista com todos os usuarios de um mesmo tipo</returns>
        List<UsuariosDomain> Listar(EnTipoUsuario tipoUsuario);

        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario a ser cadastrado</param>
        void Cadastrar(UsuariosDomain usuario);

        /// <summary>
        /// Altera as informações de um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario com os novos valores ja inseridos</param>
        void Alterar(UsuariosDomain usuario);

        /// <summary>
        /// Retorna um usuario que contenha a combinação de email e senha no banco de dadoss
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="senha">Senha do usuario</param>
        /// <returns>Um usuario que tenha o email e senha inseridos , caso não exista , retorna null</returns>
        UsuariosDomain Logar(string email,string senha);
    }
}
