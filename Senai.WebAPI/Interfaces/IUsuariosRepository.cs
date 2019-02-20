using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    interface IUsuariosRepository {

        List<UsuariosDomain> Listar();

        void Cadastrar(UsuariosDomain usuario);

        void Alterar(UsuariosDomain usuario);

        bool Remover(UsuariosDomain usuario); 

        UsuariosDomain BuscarPorEmailSenha(string email,string senha);
    }
}
