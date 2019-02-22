using Senai.WebAPI.Domains;

namespace Senai.WebAPI.Interfaces {
    interface IUsuariosRepository {

        //List<UsuariosDomain> Listar(); Listar removido 

        void Cadastrar(UsuariosDomain usuario);

        void Alterar(UsuariosDomain usuario);

        UsuariosDomain BuscarPorEmailSenha(string email,string senha);
    }
}
