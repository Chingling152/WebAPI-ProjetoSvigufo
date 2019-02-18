using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai.WebAPI.Repositorios
{
    public class TiposEventosRepository : ITiposEventosRepository
    {
        /// <summary>
        /// string usada para se comunicar com o banco de dado.    
        /// Data source = local onde esta o banco de dados . 
        /// initial catalog = Banco de dado que será usado . 
        /// user = nome do usuario . 
        /// pwd = senha do usuario . 
        /// </summary>
        private const string conexao = "Data Source = .\\SqlExpress; initial catalog = SENAI_SVIGUFO_MANHA;user = sa; pwd = 132";

        public void Alterar(TiposEventosDomain evento)
        {
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string comando = "UPDATE TIPO_EVENTO SET NOME = @Nome WHERE ID = @Id;";//Comando para atualizar o banco de dados

                connection.Open();// abre o banco de dados

                SqlCommand cmd = new SqlCommand(comando, connection);//busca connection que executa o comando na string conexão

                //substitui os valores nos @ para valores do evento 
                cmd.Parameters.AddWithValue("@Nome", evento.Nome);
                cmd.Parameters.AddWithValue("@Id", evento.ID);

                cmd.ExecuteNonQuery();//executa o comando como non-query , ou seja , ele não vai retornar valor (só vai executar uma ação)

                connection.Close();// fecha o banco de dados (opcional)
            }
        }

        /// <summary>
        /// Cadastra um evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento a ser cadastrado</param>
        public void Cadastrar(TiposEventosDomain evento)
        {
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string comando = "INSERT INTO TIPO_EVENTO VALUES(@Nome)";

                connection.Open();// abre o banco de dados

                SqlCommand cmd = new SqlCommand(comando,connection);//busca connection que executa o comando na string conexão
                cmd.Parameters.AddWithValue("@Nome",evento.Nome);
                
                cmd.ExecuteNonQuery();//executa o comando como non-query , ou seja , ele não vai retornar valor

                connection.Close();// fecha o banco de dados (opcional)
            }
        }

        public void Deletar(int ID)
        {
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string comando = "DELETE FROM TIPO_EVENTO WHERE ID = @Id";

                SqlCommand cmd = new SqlCommand(comando,connection);

                cmd.Parameters.AddWithValue("@Id",ID);

                connection.Open();

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista todos os eventos que estao no banco de dados
        /// </summary>
        /// <returns></returns>
        public List<TiposEventosDomain> Listar()
        {
            //cria lista de tipos de evento vazio (para retorno)
            List<TiposEventosDomain> rt = new List<TiposEventosDomain>();

            //usa os comandos o da string conexão para entrar no anco de dados SQL
            using (SqlConnection connection = new SqlConnection(conexao)){
                string SQLQuery = "SELECT ID,NOME FROM TIPO_EVENTO";
                connection.Open();

                SqlDataReader reader;//leitor de SQL

                //aplica os comandos da variavel SQLQuery na conexão com o banco de dados acima
                using (SqlCommand cmd = new SqlCommand(SQLQuery,connection))
                {
                    reader = cmd.ExecuteReader();//executa o comando do cmd
                    while (reader.Read())
                    {
                        rt.Add(new TiposEventosDomain(){
                            ID = Convert.ToInt32(reader["ID"]),//o valor vem no tipo object e então deve ser convertido para inteiro
                            Nome = reader["NOME"].ToString()
                        });
                    }
                }
            }

            return rt;
        }

        /// <summary>
        /// Retorna o tipo evento no banco de dados
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public TiposEventosDomain ListarPorID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                string SQLQuery = "SELECT ID,NOME FROM TIPO_EVENTO WHERE ID = @Id";
                
                SqlCommand cmd = new SqlCommand(SQLQuery,connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();//executa o comando do cmd

                while (reader.Read())
                {
                    return new TiposEventosDomain()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nome = reader["NOME"].ToString()
                    };
                }
            }
            
            return null;
        }
    }
}
