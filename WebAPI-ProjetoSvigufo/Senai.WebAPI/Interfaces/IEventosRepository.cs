using System.Collections.Generic;
using Senai.WebAPI.Domains;

namespace Senai.WebAPI.Interfaces {
    public interface IEventosRepository {

        /// <summary>
        /// Lista todos os eventos cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma lista de eventos</returns>
        List<EventosDomain> Listar();

        /// <summary>
        /// Mostra todas as informações de um evento selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID do evento selecionado</param>
        /// <returns>Um evento no ID selecionado</returns>
        EventosDomain Listar(int ID);
    
        /// <summary>
        /// Lista todos os eventos datados para hoje
        /// </summary>
        /// <returns>Uma lista de eventos</returns>
        List<EventosDomain> ListarHoje();

        /// <summary>
        /// Cadastra um evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento que sera cadastrado</param>
        void Inserir(EventosDomain evento);

        /// <summary>
        /// Altera as informações de um evento
        /// </summary>
        /// <param name="evento">Evento com os valores já alterados</param>
        void Alterar(EventosDomain evento);
    }
}
