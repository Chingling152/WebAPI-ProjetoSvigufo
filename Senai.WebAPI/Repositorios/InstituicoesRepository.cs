using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class InstituicoesRepository : IInstituicoesRepository {

        private const string StringConexao = "Data Source = .\\SQLEXPRESS; initial catalog = ; user id = sa ; user pwd = 132";

        public void Editar(InstituicoesDomain instituicao) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "UPDATE FROM INSTITUICOES SET NOME_FANTASIA = @NOME_FANTASIA, RAZAO_SOCIAL = @RAZAO_SOCIAL ,LOGRADOURO = @LOGRADOURO , CEP = @CEP , CNPJ = @CNPJ, UF = @UF ,CIDADE = @CIDADE WHERE ID = @ID";
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);

                cmd.Parameters.AddWithValue("@NOME_FANTASIA",instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL",instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@LOGRADOURO",instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP",instituicao.CEP);
                cmd.Parameters.AddWithValue("@UF",instituicao.UF);
                cmd.Parameters.AddWithValue("@CIDADE",instituicao.Cidade);
                cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);

                cmd.ExecuteNonQuery();
            }
        }

        public void Inserir(InstituicoesDomain instituicao) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "INSERT INTO INSTITUICOES VALUES (@NOME_FANTASIA,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
                cmd.Parameters.AddWithValue("@UF", instituicao.UF);
                cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
                cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);

                cmd.ExecuteNonQuery();
            }

        }

        public List<InstituicoesDomain> Listar() {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "SELECT * FROM VerInstituicoes";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<InstituicoesDomain> instituicoes= new List<InstituicoesDomain>();
                    while (leitor.Read()) {
                        instituicoes.Add(
                            new InstituicoesDomain(){
                                ID = Convert.ToInt32(leitor["ID"]),
                                NomeFantasia = leitor["NOME"].ToString(),
                                RazaoSocial = leitor["RAZAO_SOCIAL"].ToString(),
                                Logradouro = leitor["LOGRADOURO"].ToString(),
                                CEP = leitor["CEP"].ToString(),
                                UF = leitor["UF"].ToString(),
                                Cidade = leitor["CIDADE"].ToString(),
                                CNPJ = leitor["CNPJ"].ToString()
                            }
                        );
                    }
                    return instituicoes;
                }
            }
            return null;
        }
    }
}
