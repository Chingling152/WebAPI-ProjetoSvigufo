using Senai.WebAPI.Domains;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces
{
    public interface IInstituicoesRepository
    {
        /// <summary>
        /// Mostra todas as instituições do banco de dados
        /// </summary>
        /// <returns>Ula lista com os dados de todas as instituições</returns>
        List<InstituicoesDomain> Listar();

        /// <summary>
        /// Retorna todos os dados de uma instituição selecionadad pelo ID
        /// </summary>
        /// <param name="id">ID da instituição a ser procurada</param>
        /// <returns>Retorna todos os dados da instituição , caso não exista, retorna null</returns>
        InstituicoesDomain BuscarPorId(int id);

        /// <summary>
        /// Insere uma instituição no final do banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser inserida</param>
        void Inserir(InstituicoesDomain instituicao);

        /// <summary>
        /// Muda todos os valores de uma instituição no banco de dados
        /// </summary>
        /// <param name="instituicao">Nova instituição</param>
        void Editar(InstituicoesDomain instituicao);

        /// <summary>
        /// Remove uma instituição do banco de dados
        /// </summary>
        /// <param name="ID">ID da instituição</param>
        void Deletar(int ID);
    }
}
