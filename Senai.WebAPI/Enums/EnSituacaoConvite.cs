namespace Senai.WebAPI.Enums {
    /// <summary>
    /// Enumeração que representa o estado de um convite
    /// </summary>
    public enum EnSituacaoConvite {
        /// <summary>
        /// Representa um convite que foi aceito pelo destinatario
        /// </summary>
        Aceito = 1,
        /// <summary>
        /// Representa um convite que foi recusado pelo destinatario
        /// </summary>
        Recusado = 2,
        /// <summary>
        /// Representa um convite que aguarda resposta do destinatario
        /// </summary>
        Aguardando = 3
    }
}
