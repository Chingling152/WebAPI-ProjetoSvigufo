using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    public interface IConvitesRepository {

        /// <summary>
        /// Lista todos os convites
        /// </summary>
        /// <returns>Uma lista com todos os convites do banco de dados</returns>
        List<ConvitesDomain> Listar();

        /// <summary>
        /// Cadastra um convite no banco de dados
        /// </summary>
        /// <param name="convite">Convite que será cadastrado</param>
        void Cadastrar(ConvitesDomain convite);

        /// <summary>
        /// Lista todos os convites de um certo usuario
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <returns>Uma lista com todos os convites destinados a este ID</returns>
        List<ConvitesDomain> MeusConvites(int ID);

        /// <summary>
        /// Altera as informações de um convite 
        /// </summary>
        /// <param name="convite">Convite com os valores alterados</param>
        void Alterar(ConvitesDomain convite);
    }
}
