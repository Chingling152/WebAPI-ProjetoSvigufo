﻿using Senai.WebAPI.Domains;
using Senai.WebAPI.ViewModels;
using System.Collections.Generic;

namespace Senai.WebAPI.Interfaces
{
    /// <summary>
    /// Interface que lida com dados de instituições
    /// </summary>
    public interface IInstituicoesRepository
    {
        /// <summary>
        /// Mostra todas as instituições do banco de dados
        /// </summary>
        /// <returns>Uma lista com os dados de todas as instituições</returns>
        List<InstituicoesDomain> Listar();
        
        /// <summary>
        /// Lista todas as instituições que tenha um nome parecido com o inserido
        /// </summary>
        /// <param name="Nome">Nome fantasia da instituição</param>
        /// <returns>Retorna uma lista com todas as instituições com o nome parecido com o inserido</returns>
        List<InstituicoesViewModel> Listar(string nome);

        /// <summary>
        /// Retorna todos os dados de uma instituição selecionada pelo ID
        /// </summary>
        /// <param name="id">ID da instituição a ser procurada</param>
        /// <returns>Retorna todos os dados da instituiçãoreturns>
        InstituicoesDomain Listar(int id);

        /// <summary>
        /// Insere uma instituição no final do banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser inserida</param>
        void Cadastrar(InstituicoesDomain instituicao);

        /// <summary>
        /// Muda todos os valores de uma instituição no banco de dados
        /// </summary>
        /// <param name="instituicao">Nova instituição</param>
        void Atualizar(InstituicoesDomain instituicao);
    }
}
