namespace Senai.WebAPI.Enums {
    /// <summary>
    /// Enumeração responsavel por mostrar os estados de um Evento
    /// </summary>
    public enum EnSituacaoEvento {
        /// <summary>
        /// Representa um evento que não está aceitando mais pessoas
        /// </summary>
        Fechado = 0,
        /// <summary>
        /// Representa um evento que está aceitando pessoas
        /// </summary>
        Aberto = 1,
        /// <summary>
        /// Representa um evento cancelado
        /// </summary>
        Cancelado = 2,
        /// <summary>
        /// Representa um evento que já ocorreu
        /// </summary>
        Terminado = 3
    }
}
