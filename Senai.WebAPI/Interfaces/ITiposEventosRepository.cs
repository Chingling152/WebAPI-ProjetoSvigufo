using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces
{
    interface ITiposEventosRepository
    {
        /// <summary>
        /// Lista todos os tipo de de eventos
        /// </summary>
        /// <returns>Uma lista contendo todos os eventos</returns>
        List<TiposEventosDomain> Listar();

        /// <summary>
        /// Cadastra um evento na lista de eventos
        /// </summary>
        /// <param name="evento">Evento a ser cadastrado</param>
        void Cadastrar(TiposEventosDomain evento);

        /// <summary>
        /// Altera um tipo de evento cadastrado no banco de dados
        /// </summary>
        /// <param name="evento"></param>
        void Alterar(TiposEventosDomain evento);

        /// <summary>
        /// Busca o tipo de evento com o ID selecionado
        /// </summary>
        /// <param name="ID">ID do tipo de evento</param>
        /// <returns>Retorna um tipo de evento</returns>
        TiposEventosDomain ListarPorID(int ID);

        /// <summary>
        /// Remove o tipo de evento no ID selecionado
        /// </summary>
        /// <param name="ID">ID do tipo de evento a ser deletado</param>
        void Deletar(int ID);
    }
}
