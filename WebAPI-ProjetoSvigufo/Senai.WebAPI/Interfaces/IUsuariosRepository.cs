using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    interface IUsuariosRepository {

        /// <summary>
        /// Mostra todos os usuarios cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        List<UsuariosDomain> Listar();

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
        UsuariosDomain BuscarPorEmailSenha(string email,string senha);
    }
}
