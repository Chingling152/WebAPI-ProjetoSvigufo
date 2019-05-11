using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces {
    public interface IConvitesRepository {

        /// <summary>
        /// Lista todos os convites ordenados por data
        /// </summary>
        /// <returns>Uma lista com todos os convites do banco de dados</returns>
        List<ConvitesDomain> Listar();

        /// <summary>
        /// Lista um unico convite
        /// </summary>
        /// <param name="ID">ID do Convite</param>
        /// <returns>Retorna todas as informações de um convite</returns>
        ConvitesDomain Listar(int ID);

        /// <summary>
        /// Lista uma certa quantidade de convites
        /// </summary>
        /// <param name="pagina">A partir de qual registro será contado</param>
        /// <param name="quantidade">Quantidade de convites que serão retornados</param>
        /// <returns>Uma lista com N convites do banco de dados</returns>
        List<ConvitesDomain> Listar(int pagina,int quantidade);

        /// <summary>
        /// Lista uma quantidade de convites com uma determinada situação
        /// </summary>
        /// <param name="pagina">A partir de qual indice será retornado</param>
        /// <param name="quant">Quantos convites serão retornados</param>
        /// <param name="situacao">Apenas convites com essa situação serão retornados</param>
        /// <returns>Uma lista com N convites de uma mesma situação</returns>
        List<ConvitesDomain> Listar(int pagina, int quant, EnSituacaoConvite situacao);

        /// <summary>
        /// Retorna todos os convidados de um convite
        /// </summary>
        /// <param name="ID">ID do evento</param>
        /// <returns>Uma lista com todos os convites do evento</returns>
        List<ConvitesDomain> ListarConvidados(int ID);
        
        /// <summary>
        /// Lista uma quantidade convidados do evento
        /// </summary>
        /// <param name="ID">ID do evento</param>
        /// <param name="pagina">Quantos registros serão pulados (ignorados)</param>
        /// <param name="quantidade">Quantos registros serão retornados</param>
        /// <returns>Retorna uma lista com N[quantidade] convites a partir do M[pagina] registro</returns>
        List<ConvitesDomain> ListarConvidados(int ID,int pagina, int quantidade);

        /// <summary>
        /// Cadastra um convite no banco de dados
        /// </summary>
        /// <param name="convite">Convite que será cadastrado</param>
        void Cadastrar(ConvitesDomain convite);

        /// <summary>
        /// Lista uma quantidade de convites de um Usuario
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <param name="quant">quantidade de convites que será retornado</param>
        /// <param name="pagina">A partir de qual registro será contado</param>
        /// <returns>Retorna N convites de um usuario a partir de N registro</returns>
        List<ConvitesDomain> MeusConvites(int ID, int pagina, int quant);

        /// <summary>
        /// Lista uma quantidade de convites de um usuario de uma determinada situação
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <param name="pagina">numero da pagina procurada</param>
        /// <param name="quant">quantidade de itens que será retornada</param>
        /// <param name="situacao">situação de todos os convites que serão procurados</param>
        /// <returns>Retorna uma lista com todos os convites com a mesma situação</returns>
        List<ConvitesDomain> MeusConvites(int ID, int pagina, int quant, EnSituacaoConvite situacao);

        /// <summary>
        /// Lista todos os eventos de um usuario onde ele é o palestrante
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <returns>Retorna uma lista com todos os eventos de um palestrante</returns>
        List<ConvitesDomain> MinhasPalestras(int ID, int pagina, int quant);

        /// <summary>
        /// Altera as informações de um convite 
        /// </summary>
        /// <param name="convite">Convite com os valores alterados</param>
        void Alterar(ConvitesDomain convite);
    }
}
