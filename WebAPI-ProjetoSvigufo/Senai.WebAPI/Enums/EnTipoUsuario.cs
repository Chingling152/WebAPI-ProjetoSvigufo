namespace Senai.WebAPI.Enums {
    /// <summary>
    /// Enumeração que se refere aos tipos de usuario da API
    /// </summary>
    public enum EnTipoUsuario {
        /// <summary>
        /// Representa um usuario comum , pode visualizar e enviar convites
        /// </summary>
        Usuario = 1,
        /// <summary>
        /// Representa um usuario palestrante , pode criar eventos e tudo que o usuario comum pode fazer
        /// </summary>
        Palestrante = 2,
        /// <summary>
        /// Representa um administrador do sistema , pode alterar e visualizar todos os dados
        /// </summary>
        Administrador = 3
    }
}
