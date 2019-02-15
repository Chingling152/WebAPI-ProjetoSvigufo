using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai.WebAPI.Repositorios
{
    public class InstituicoesRepository : IInstituicoesRepository
    {
        private const string conexao = "Data Source = .\\SqlExpress; initial catalog = SVIGUFO;user = sa ; pwd = 132";

        public InstituicaoDomain BuscarPorId(int id)
        {
            SqlConnection cnx = new SqlConnection(conexao);

            SqlCommand cmd = new SqlCommand("SELECT * FROM VerInstituicoes WHERE ID = @ID", cnx);
            cmd.Parameters.AddWithValue("@ID", id);
            
            cnx.Open();

            using (SqlDataReader leitor = cmd.ExecuteReader())
            {
                while (leitor.Read())
                {
                    return new InstituicaoDomain(){
                        ID = Convert.ToInt32(leitor["ID"]),
                        NomeFantasia = leitor["NOME_FANTASIA"].ToString(),
                        Logradouro = leitor["LOGRADOURO"].ToString(),
                        CEP = Convert.ToInt32(leitor["CEP"]),
                        UF = leitor["UF"].ToString(),
                        Cidade = leitor["CIDADE"].ToString()
                   };
                }
            }
            return null;
        }

        public void Deletar(int ID)
        {
            SqlConnection cnx = new SqlConnection(conexao);
            string deletar = "DELETE FROM INSTITUICAO WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(deletar, cnx);
            cnx.Open();
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
        }

        public void Editar(InstituicaoDomain instituicao)
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

        public void Inserir(InstituicaoDomain instituicao)
        {
            SqlConnection cnx = new SqlConnection(conexao);
            string insert = "INSERT INTO INSTITUICAO VALUES(@NOME,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";

            SqlCommand cmd = new SqlCommand(insert, cnx);

            cmd.Parameters.AddWithValue("@NOME", instituicao.NomeFantasia);
            cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
            cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
            cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
            cmd.Parameters.AddWithValue("@UF", instituicao.UF);
            cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazãoSocial);
            cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
            cmd.Parameters.AddWithValue("@ID", instituicao.ID);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public List<InstituicaoDomain> Listar()
        {
            List<InstituicaoDomain> lista = new List<InstituicaoDomain>();

            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string comando = "SELECT * FROM VerInstituicoes";
                SqlCommand cmd = new SqlCommand(comando,connection);

                connection.Open();

                SqlDataReader leitor = cmd.ExecuteReader();


                while (leitor.Read())
                {
                    lista.Add(new InstituicaoDomain() { 
                        ID = Convert.ToInt32(leitor["ID"]),
                        NomeFantasia = leitor["NOME"].ToString(),
                        Logradouro = leitor["LOGRADOURO"].ToString(),
                        CEP = Convert.ToInt32(leitor["CEP"]),
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
