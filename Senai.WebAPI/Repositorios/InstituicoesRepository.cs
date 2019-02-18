using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios
{
    public class InstituicoesRepository : IInstituicoesRepository
    {
        private const string conexao = "Data Source = .\\SqlExpress; initial catalog = SENAI_SVIGUFO_MANHA;user = sa ; pwd = 132";

        /// <summary>
        /// Busca uma instituição no banco de dados
        /// </summary>
        /// <param name="id">ID da instituição a ser procurada</param>
        /// <returns>Retorna a instituição no ID selecionado , caso não exista , retorna null</returns>
        public InstituicoesDomain BuscarPorId(int id)
        {
            SqlConnection cnx = new SqlConnection(conexao);

            SqlCommand cmd = new SqlCommand("SELECT * FROM VerInstituicoes WHERE ID = @ID", cnx);
            cmd.Parameters.AddWithValue("@ID", id);
            
            cnx.Open();

            using (SqlDataReader leitor = cmd.ExecuteReader())
            {
                while (leitor.Read())
                {
                    return new InstituicoesDomain(){
                        ID = Convert.ToInt32(leitor["ID"]),
                        NomeFantasia = leitor["NOME_FANTASIA"].ToString(),
                        Logradouro = leitor["LOGRADOURO"].ToString(),
                        CEP = leitor["CEP"].ToString(),
                        UF = leitor["UF"].ToString(),
                        Cidade = leitor["CIDADE"].ToString()
                   };
                }
            }
            return null;
        }

        /// <summary>
        /// Remove uma instituição do banco de dados
        /// </summary>
        /// <param name="ID">ID Da instituição a ser removida</param>
        public void Deletar(int ID)
        {
            SqlConnection cnx = new SqlConnection(conexao);
            string deletar = "DELETE FROM INSTITUICAO WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(deletar, cnx);
            cnx.Open();
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Altera as informações de uma instituição do banco de dados
        /// </summary>
        /// <param name="instituicao">Nova instituição</param>
        public void Editar(InstituicoesDomain instituicao)
        {
            SqlConnection cnx = new SqlConnection(conexao);
            
            string edit = "UPDATE INSTITUICAO SET NOME_FANTASIA = @NOME , LOGRADOURO = @LOGRADOURO , CEP = @CEP , CIDADE = @CIDADE , UF = @UF WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(edit,cnx);
            cnx.Open();// N ESQUECE ISSAQUI N ;---;

            cmd.Parameters.AddWithValue("@NOME",instituicao.NomeFantasia);
            cmd.Parameters.AddWithValue("@LOGRADOURO",instituicao.Logradouro);
            cmd.Parameters.AddWithValue("@CEP",instituicao.CEP);
            cmd.Parameters.AddWithValue("@CIDADE",instituicao.Cidade);
            cmd.Parameters.AddWithValue("@UF",instituicao.UF);
            cmd.Parameters.AddWithValue("@ID",instituicao.ID);

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Insere uma instituição no final do banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser inserida</param>
        public void Inserir(InstituicoesDomain instituicao)
        {
            SqlConnection cnx = new SqlConnection(conexao);
            string insert = "INSERT INTO INSTITUICAO VALUES(@NOME,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";

            SqlCommand cmd = new SqlCommand(insert, cnx);

            cmd.Parameters.AddWithValue("@NOME", instituicao.NomeFantasia);
            cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
            cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
            cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
            cmd.Parameters.AddWithValue("@UF", instituicao.UF);
            cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
            cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
            cmd.Parameters.AddWithValue("@ID", instituicao.ID);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Mostra todas as instituições cadastradas no banco de dados
        /// </summary>
        /// <returns>Uma lista com todas as instituições cadastradas</returns>
        public List<InstituicoesDomain> Listar()
        {
            List<InstituicoesDomain> lista = new List<InstituicoesDomain>();

            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string comando = "SELECT * FROM VerInstituicoes";
                SqlCommand cmd = new SqlCommand(comando,connection);

                connection.Open();

                SqlDataReader leitor = cmd.ExecuteReader();


                while (leitor.Read())
                {
                    lista.Add(new InstituicoesDomain() { 
                        ID = Convert.ToInt32(leitor["ID"]),
                        NomeFantasia = leitor["NOME"].ToString(),
                        Logradouro = leitor["LOGRADOURO"].ToString(),
                        CEP = leitor["CEP"].ToString(),
                        UF = leitor["UF"].ToString(),
                        Cidade = leitor["CIDADE"].ToString()
                        }
                    );
                }
            }

            return lista;
        }
    }
}
