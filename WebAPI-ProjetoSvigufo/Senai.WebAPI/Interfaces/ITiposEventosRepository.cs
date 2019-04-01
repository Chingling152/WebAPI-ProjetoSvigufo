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
        /// <param name="Tipoevento">Evento a ser cadastrado</param>
        void Cadastrar(TiposEventosDomain Tipoevento);
        /// <summary>
        /// Altera um tipo de evento cadastrado no banco de dados
        /// </summary>
        /// <param name="Tipoevento">Tipo de evento com os valores ja inseridos</param>
        void Alterar(TiposEventosDomain Tipoevento);
    }
}
