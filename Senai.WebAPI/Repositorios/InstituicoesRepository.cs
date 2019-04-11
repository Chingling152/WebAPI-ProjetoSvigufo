using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    /// <summary>
    /// Classe que lida com dados relacionado a instituições
    /// </summary>
    public class InstituicoesRepository : IInstituicoesRepository {

        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;Integrated Security = true";

        /// <summary>
        /// Busca no banco de dados todas as instituições cadastradas e retorna em forma de lista
        /// </summary>
        /// <returns>Retorna todas as instituições cadastradas no banco de dados</returns>
        public List<InstituicoesDomain> Listar() {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM INSTITUICOES";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<InstituicoesDomain> instituicoes = new List<InstituicoesDomain>();
                    while (leitor.Read()) {
                        instituicoes.Add(
                            new InstituicoesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                NomeFantasia = leitor["NOME_FANTASIA"].ToString(),
                                CEP = leitor["CEP"].ToString(),
                                CNPJ = leitor["CNPJ"].ToString(),
                                Logradouro = leitor["LOGRADOURO"].ToString(),
                                Cidade = leitor["CIDADE"].ToString(),
                                UF = leitor["UF"].ToString(),
                                RazaoSocial = leitor["RAZAO_SOCIAL"].ToString()
                            }  
                        );
                    }
                    return instituicoes;
                }

            }
            throw new NullReferenceException("Não existe Instituições cadastradas no banco de dados");
        }

        /// <summary>
        /// Procura uma instituição cadastrada no ID selecionado
        /// </summary>
        /// <param name="id">ID da instituição selecionada</param>
        /// <returns>Uma instituição que esteja cadastrada no ID selecionado , ou retorna null caso não exista registros neste ID</returns>
        public InstituicoesDomain Listar(int id) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM INSTITUICOES";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new InstituicoesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                NomeFantasia = leitor["NOME_FANTASIA"].ToString(),
                                CEP = leitor["CEP"].ToString(),
                                CNPJ = leitor["CNPJ"].ToString(),
                                Logradouro = leitor["LOGRADOURO"].ToString(),
                                Cidade = leitor["CIDADE"].ToString(),
                                UF = leitor["UF"].ToString(),
                                RazaoSocial = leitor["RAZAO_SOCIAL"].ToString()
                        };
                    }
                }

            }
            throw new NullReferenceException("Não existe Instituição no ID selecionado");
        }

        /// <summary>
        /// Atualiza os valores de uma instituição no banco de dados , Deve se inserir o ID par asaber qual instituição será alterada
        /// </summary>
        /// <param name="instituicao">Instituição com os valores já alterados</param>
        public void Atualizar(InstituicoesDomain instituicao) {
                using (SqlConnection conexao = new SqlConnection(Conexao)) {
                    string comando = "Atualizar @ID , @NOME_FANTASIA , @RAZAO_SOCIAL , @CNPJ , @LOGRADOURO , @CEP , @UF , @CIDADE";
                    conexao.Open();
                    SqlCommand cmd = new SqlCommand(comando, conexao);
                    cmd.Parameters.AddWithValue("@ID", instituicao.ID);
                    cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                    cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                    cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
                    cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                    cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
                    cmd.Parameters.AddWithValue("@UF", instituicao.UF);
                    cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
                    cmd.ExecuteNonQuery();
                }
            
        }

        /// <summary>
        /// Cadastra uma instituição no banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser cadastrada</param>
        public void Cadastrar(InstituicoesDomain instituicao) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "CriarInstituicao @NOME_FANTASIA , @RAZAO_SOCIAL , @CNPJ , @LOGRADOURO , @CEP , @UF , @CIDADE";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
                cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
                cmd.Parameters.AddWithValue("@UF", instituicao.UF);
                cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
