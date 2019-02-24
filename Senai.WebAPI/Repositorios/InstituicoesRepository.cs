using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios
{
    /// <summary>
    /// Classe que manuseia dados de insituições atraves de um banco de dados
    /// </summary>
    public class InstituicoesRepository : IInstituicoesRepository
    {

        /// <summary>
        /// string usada para se comunicar com o banco de dado.    
        /// Data source = local onde esta o banco de dados . 
        /// initial catalog = Banco de dado que será usado . 
        /// user = nome do usuario . 
        /// pwd = senha do usuario . 
        /// </summary>
        private const string conexao = "Data Source = .\\SqlExpress; initial catalog = SENAI_SVIGUFO_MANHA;user = sa ; pwd = 132";

        /// <summary>
        /// Busca uma instituição no banco de dados
        /// </summary>
        /// <param name="id">ID da instituição a ser procurada</param>
        /// <returns>Retorna a instituição no ID selecionado , caso não exista , retorna null</returns>
        public InstituicoesDomain BuscarPorId(int id)
        {
            using(SqlConnection cnx = new SqlConnection(conexao)) {// maneira adequada de abrir e fechar um banco de dados
                SqlCommand cmd = new SqlCommand("SELECT * FROM VerInstituicoes WHERE ID = @ID", cnx);
                cnx.Open();//abre uma conexão com o banco de dados 
                cmd.Parameters.AddWithValue("@ID", id);
            
                using (SqlDataReader leitor = cmd.ExecuteReader())
                {
                    if (!leitor.HasRows)
                        return null;
                    
                    while (leitor.Read())
                    {
                        return new InstituicoesDomain(){
                            ID = Convert.ToInt32(leitor["ID"]),
                            NomeFantasia = leitor["NOME"].ToString(),
                            Logradouro = leitor["LOGRADOURO"].ToString(),
                            CEP = leitor["CEP"].ToString(),
                            UF = leitor["UF"].ToString(),
                            Cidade = leitor["CIDADE"].ToString()
                       };
                    }
                }
            }//fecha automaticamente a conexão com banco de dados
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
            cnx.Open();//maneira rude de abrir conexões com banco de dados
            cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
            cnx.Close();//maneira rude de fechar conexões com banco de dados
        }

        /// <summary>
        /// Altera as informações de uma instituição do banco de dados
        /// </summary>
        /// <param name="instituicao">Nova instituição</param>
        public void Editar(InstituicoesDomain instituicao)
        {
            using(SqlConnection cnx = new SqlConnection(conexao)) {
                string edit = "UPDATE INSTITUICAO SET NOME_FANTASIA = @NOME , LOGRADOURO = @LOGRADOURO , CEP = @CEP , CIDADE = @CIDADE , UF = @UF WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(edit,cnx);
                cnx.Open();
                //substitui todos os paramentros com @ por variaveis
                cmd.Parameters.AddWithValue("@NOME",instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@LOGRADOURO",instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP",instituicao.CEP);
                cmd.Parameters.AddWithValue("@CIDADE",instituicao.Cidade);
                cmd.Parameters.AddWithValue("@UF",instituicao.UF);
                cmd.Parameters.AddWithValue("@ID",instituicao.ID);
                //executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Insere uma instituição no final do banco de dados
        /// </summary>
        /// <param name="instituicao">Instituição a ser inserida</param>
        public void Inserir(InstituicoesDomain instituicao)
        {
            using(SqlConnection cnx = new SqlConnection(conexao)) {
                string insert = "INSERT INTO INSTITUICAO VALUES(@NOME,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";

                SqlCommand cmd = new SqlCommand(insert, cnx);
                cnx.Open();//abre uma conexão com o banco de dados

                cmd.Parameters.AddWithValue("@NOME", instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
                cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
                cmd.Parameters.AddWithValue("@UF", instituicao.UF);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
                cmd.Parameters.AddWithValue("@ID", instituicao.ID);

                cmd.ExecuteNonQuery();
            }
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

                if (!leitor.HasRows)
                    return null;

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
