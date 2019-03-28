using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Repositorios {
    /// <summary>
    /// Classe usada para inserir , alterar e listar eventos do banco de dados 
    /// </summary>
    public class EventosRepository : IEventosRepository {

        /// <summary>
        /// string usada para se comunicar com o banco de dado.  
        /// Data source = local onde esta o banco de dados . 
        /// initial catalog = Banco de dado que será usado . 
        /// user = nome do usuario . 
        /// pwd = senha do usuario . 
        /// </summary>
        private const string conexao = "Data Source=.\\SQLEXPRESS; initial catalog = SENAI_SVIGUFO_MANHA;user id = sa; pwd = 132";

        /// <summary>
        /// Altera as informações de um evento
        /// </summary>
        /// <param name="evento">Evento já alterado</param>
        public void Alterar(EventosDomain evento) {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "UPDATE EVENTOS SET NOME = @NOME , DESCRICAO = @DESCRICAO , DATA_EVENTO = @DATA_EVENTO , ACESSO_LIVRE = @ACESSO_LIVRE,ID_INSTITUICAO = @ID_INSTITUICAO,ID_TIPO_EVENTO = @ID_TIPO_EVENTO, CANCELADO = @CANCELADO";
                SqlCommand cmd = new SqlCommand(comando,connection);

                cmd.Parameters.AddWithValue("@NOME", evento.Nome);
                cmd.Parameters.AddWithValue("@DESCRICAO", evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO", evento.DataEvento);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO", evento.Instituicao.ID);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO", evento.TipoEvento.ID);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE", evento.AcessoLivre);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Insere um novo evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento a ser enviado para o banco</param>
        public void Inserir(EventosDomain evento) {
            using(SqlConnection connection = new SqlConnection(conexao)){
                string comando = "INSERT INTO EVENTOS(NOME,DESCRICAO,DATA_EVENTO,ID_INSTITUICAO,ID_TIPO_EVENTO,ACESSO_LIVRE,CANCELADO) VALUES(@NOME,@DESCRICAO,@DATA_EVENTO,@ID_INSTITUICAO,@ID_TIPO_EVENTO,@ACESSO_LIVRE,@CANCELADO)";
                SqlCommand cmd = new SqlCommand(comando,connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@NOME",evento.Nome);
                cmd.Parameters.AddWithValue("@DESCRICAO",evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO",evento.DataEvento);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO",evento.Instituicao.ID);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO",evento.TipoEvento.ID);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE",evento.AcessoLivre);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mostra todos os eventos salvos no banco de dados
        /// </summary>
        /// <returns>Uma list acom todos os eventos do banco de dados</returns>
        public List<EventosDomain> Listar() {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerEventos";
                connection.Open();

                SqlCommand cmd = new SqlCommand(comando,connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (!leitor.HasRows)
                    throw new NullReferenceException("Não existe nenhum evento cadastrado");

                List<EventosDomain> lista = new List<EventosDomain>();
                while (leitor.Read()) {
                    lista.Add(
                       new EventosDomain() {
                           ID = Convert.ToInt32(leitor["EVENTO"]),
                           Nome = leitor["NOME_EVENTO"].ToString(),
                           Descricao = leitor["DESCRICAO"].ToString(),
                           AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                           DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                            //Criação do objeto TipoEvento
                            TipoEvento = new TiposEventosDomain() {
                               ID = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                               Nome = leitor["TIPO_EVENTO"].ToString()
                           },
                            //Criação do objeto Instituição
                            Instituicao = new InstituicoesViewModel() {
                               ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                               Nome = leitor["NOME_INSTITUICAO"].ToString(),
                               CEP = leitor["CEP"].ToString(),
                               Cidade = leitor["CIDADE"].ToString()
                           }
                       }
                   );
                }
                return lista;

            }
        }

        /// <summary>
        /// Lista todos eventos no dia de hoje
        /// </summary>
        /// <returns>Uma lista de eventos</returns>
        public List<EventosDomain> ListarHoje() {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerEventos WHERE DAY(DATA_EVENTO) = DAY(GETDATE())";
                connection.Open();

                SqlCommand cmd = new SqlCommand(comando, connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (!leitor.HasRows)
                    throw new NullReferenceException("Não existe nenhum evento neste dia");

                List<EventosDomain> lista = new List<EventosDomain>();
                while (leitor.Read()) {
                    lista.Add(
                        new EventosDomain() {
                            ID = Convert.ToInt32(leitor["EVENTO"]),
                            Nome = leitor["NOME_EVENTO"].ToString(),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                            DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                            //Criação do objeto TipoEvento
                            TipoEvento = new TiposEventosDomain() {
                                ID = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                Nome = leitor["TIPO_EVENTO"].ToString()
                            },
                            //Criação do objeto Instituição
                            Instituicao = new InstituicoesViewModel() {
                                ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                CEP = leitor["CEP"].ToString(),
                                Cidade = leitor["CIDADE"].ToString()
                            }
                        }
                    );
                }
                return lista;

            }
        }
    }
}
