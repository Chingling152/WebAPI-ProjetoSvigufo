using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    public interface IConvitesRepository {
        List<ConvitesDomain> Listar();
        void Cadastrar(ConvitesDomain convite);
        List<ConvitesDomain> ListarMeusConvites(int ID);
        void AlterarSituacao(ConvitesDomain convite);
    }
}
