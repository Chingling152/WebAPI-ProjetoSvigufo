using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class TiposEventosRepository : ITiposEventosRepository {
        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;user id = sa;pwd = 132";

        /// <summary>
        /// Altera os valores de um tipo de evento no banco de dados
        /// </summary>
        /// <param name="Tipoevento">Tipo de evento </param>
        public void Alterar(TiposEventosDomain Tipoevento) {
            if(Tipoevento.ID!= 0) { 
                using (SqlConnection conexao = new SqlConnection(Conexao)) {
                    string comando = "AtualizarTipoEvento @ID @NOME";
                    conexao.Open();
                    SqlCommand cmd = new SqlCommand(comando, conexao);
                    cmd.Parameters.AddWithValue("@ID", Tipoevento.ID);
                    cmd.Parameters.AddWithValue("@NOME", Tipoevento.Nome);
                    cmd.ExecuteNonQuery();
                }
            }
            throw new NullReferenceException("Você precisa inserir o ID do Tipo de Evento que você quer alterar");
        }

        /// <summary>
        /// Cadastra um tipo de evento no banco de dados
        /// </summary>
        /// <param name="Tipoevento">Tipo evento a ser cadastrado</param>
        public void Cadastrar(TiposEventosDomain Tipoevento) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "CriarTipoEvento @NOME";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME", Tipoevento.Nome);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista todos os tipos de eventos do banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os tipos de eventos existentes no banco de dados</returns>
        public List<TiposEventosDomain> Listar() {
            using(SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM TIPOS_EVENTOS";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando,conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<TiposEventosDomain> TiposEventos = new List<TiposEventosDomain>();
                    while (leitor.Read()) {
                        TiposEventos.Add(
                            new TiposEventosDomain(){
                                ID = Convert.ToInt32(leitor["ID"]),
                                Nome = leitor["NOME"].ToString()
                            }  
                        );
                    }
                    return TiposEventos;
                }

            }
            throw new NullReferenceException("Não existe dados cadastrados no banco");
        }
    }
}
