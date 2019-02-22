using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class TiposEventosRepository : ITiposEventosRepository {

        private const string StringConexao = "Data Source = .\\SQLEXPRESS; initial catalog = ; user id = sa ; user pwd = 132";

        /// <summary>
        /// Altera os valores de um determinado tipo de evento (Apenas admin)
        /// </summary>
        /// <param name="evento">Novo valor para o evento</param>
        public void Alterar(TiposEventosDomain evento) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "UPDATE TIPOS_USUARIOS SET NOME = @NOME WHERE ID = @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", evento.ID);
                cmd.Parameters.AddWithValue("@NOME", evento.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra um novo tipo de evento no banco de dados (Apenas admin)
        /// </summary>
        /// <param name="evento">Evento a ser mandado para o banco de dados</param>
        public void Cadastrar(TiposEventosDomain evento) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "INSERT INTO TIPOS_USUARIOS(NOME) VALUES (@NOME)";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME",evento.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Remove um tipo de evento do banco de dados (Apenas admin)
        /// </summary>
        /// <param name="ID">ID do tipo de evento</param>
        public void Deletar(int ID) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "DELETE FROM TIPOS_USUARIOS WHERE ID = @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID",ID);
                cmd.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// Mostra todos os tipos de eventos cadastrados no banco de dados (Apenas admin)
        /// </summary>
        /// <returns>Retorna uma lista com todos os tipos de evento , caso não exista  nenhum, retorna null</returns>
        public List<TiposEventosDomain> Listar() {

            using(SqlConnection conexao = new SqlConnection(StringConexao)) {         
                string comando = "SELECT * FROM TIPOS_USUARIOS";
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando,conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<TiposEventosDomain> tiposEventos = new List<TiposEventosDomain>();
                    while (leitor.Read()) {
                        tiposEventos.Add(
                            new TiposEventosDomain(){
                                ID = Convert.ToInt32(leitor["ID"]),
                                Nome = leitor["NOME"].ToString()
                            }
                         );
                    }
                    return tiposEventos;
                }
            }

            return null;
        }
    }
}
