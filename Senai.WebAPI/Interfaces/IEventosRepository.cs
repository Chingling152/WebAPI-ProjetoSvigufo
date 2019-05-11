using System;
using System.Collections.Generic;
using Senai.WebAPI.Domains;

namespace Senai.WebAPI.Interfaces {
    /// <summary>
    /// Interface com todos os metodos para lidar com eventos
    /// </summary>
    public interface IEventosRepository {

        /// <summary>
        /// Lista todos os eventos cadastrados no banco de dados
        /// </summary>
        /// <returns>Uma lista de eventos</returns>
        List<EventosDomain> Listar();

        /// <summary>
        /// Lista uma quantidade de Eventos  
        /// </summary>
        /// <param name="quantidade">Quantidade de eventos</param>
        /// <param name="pagina">Quantos eventos serão ignorados antes de começar a contagem</param>
        /// <returns>Retorna uma lista com N eventos</returns>
        List<EventosDomain> Listar(int quantidade,int pagina);

        /// <summary>
        /// Mostra todas as informações de um evento selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID do evento selecionado</param>
        /// <returns>Um evento no ID selecionado</returns>
        EventosDomain Listar(int ID);

        /// <summary>
        /// Lista eventos que aconteceram em um intervalo de data especifico
        /// </summary>
        /// <param name="dataInicial">Data inicial</param>
        /// <param name="dataFinal">Data final</param>
        /// <param name="quantidade">Quantidade de eventos</param>
        /// <param name="pagina">Quantos eventos serão ignorados antes de começar a contagem</param>
        /// <returns>Uma lista de eventos</returns>
        List<EventosDomain> Listar(DateTime dataInicial, DateTime dataFinal,int quantidade, int pagina);

        /// <summary>
        /// Lista todos os eventos de um tipo de evento especifico 
        /// </summary>
        /// <param name="tipoEvento">Tipo de evento dos eventos</param>
        /// <param name="quantidade">Quantidade de eventos</param>
        /// <param name="pagina">Quantos eventos serão ignorados antes de começar a contagem</param>
        /// <returns>Uma lista com todos os tipos de eventos</returns>
        List<EventosDomain> ListarTipo(int tipoEvento, int quantidade, int pagina);

        /// <summary>
        /// Lista todas os eventos localizados em uma instituição especificada
        /// </summary>
        /// <param name="instituicao">Instituição a ser procurada</param>
        /// <param name="quantidade">Quantidade de eventos</param>
        /// <param name="pagina">Quantos eventos serão ignorados antes de começar a contagem</param>
        /// <returns>Uma lista de eventos</returns>
        List<EventosDomain> ListarInstituicao(int instituicao, int quantidade, int pagina);

        /// <summary>
        /// Cadastra um evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento que sera cadastrado</param>
        void Cadastrar(EventosDomain evento);

        /// <summary>
        /// Altera as informações de um evento
        /// </summary>
        /// <param name="evento">Evento com os valores já alterados</param>
        void Alterar(EventosDomain evento);
    }
}
