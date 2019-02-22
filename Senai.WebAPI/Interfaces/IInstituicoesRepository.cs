using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces
{
    public interface IInstituicoesRepository
    {
        /// <summary>
        /// Mostra todas as instituições do banco de dados
        /// </summary>
        /// <returns>Uma lista com os dados de todas as instituições</returns>
        List<InstituicoesDomain> Listar();

        /// <summary>
        /// Insere uma instituição no final do banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser inserida</param>
        void Inserir(InstituicoesDomain instituicao);

        /// <summary>
        /// Muda os valores de uma instituição no banco de dados 
        /// </summary>
        /// <param name="instituicao">Instituição com os campos já modificados</param>
        void Editar(InstituicoesDomain instituicao);
    }
}
