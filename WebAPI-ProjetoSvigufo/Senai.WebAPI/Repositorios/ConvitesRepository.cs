using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Repositorios 
{
    /// <summary>
    /// Classe que faz alterações , cadastros e listagens de Convites no banco de dados
    /// </summary>
    public class ConvitesRepository : IConvitesRepository {

        /// <summary>
        /// string usada para se comunicar com o banco de dado.    
        /// Data source = local onde esta o banco de dados . 
        /// initial catalog = Banco de dado que será usado . 
        /// user = nome do usuario . 
        /// pwd = senha do usuario . 
        /// </summary>
        private const string Conexao = "Data Source=.\\SQLEXPRESS; initial catalog = SENAI_SVIGUFO_MANHA;user id = sa; pwd = 132";

        /// <summary>
        /// Altera o status do convite 
        /// </summary>
        /// <param name="convite">Convite com o status mudado</param>
        public void Alterar(ConvitesDomain convite) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "UPDATE FROM CONVITES SET SITUACAO = @SITUACAO WHERE ID = @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);

                cmd.Parameters.AddWithValue("@SITUACAO",(int)convite.Status);
                cmd.Parameters.AddWithValue("@ID",convite.ID);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cria um novo convite e o salva no banco de dados com o valor padrão de Situação (aguardando)
        /// </summary>
        /// <param name="convite">O Convite que será salvo no banco de dados</param>
        public void Cadastrar(ConvitesDomain convite) {
            using (SqlConnection connection = new SqlConnection(Conexao)) {
                connection.Open();

                string comando = "INSERT INTO CONVITES(ID_USUARIO,ID_EVENTO) VALUES (@ID_USUARIO,@ID_EVENTO,@SITUACAO)";
                SqlCommand cmd = new SqlCommand(comando,connection);

                cmd.Parameters.AddWithValue("@ID_USUARIO",convite.Usuario.ID);
                cmd.Parameters.AddWithValue("@ID_EVENTO",convite.Evento.ID);
                cmd.Parameters.AddWithValue("@SITUACAO",(int)convite.Status);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mostra todos os convites enviados
        /// </summary>
        /// <returns>Uma lista com todos os convites do banco de dados</returns>
        public List<ConvitesDomain> Listar() {

            using(SqlConnection connection = new SqlConnection()) {
                string comando = "SELECT * FROM VerConvites";
                SqlCommand cmd = new SqlCommand(comando,connection);
                connection.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> lista = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        lista.Add(new ConvitesDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Usuario = new UsuariosViewModel() {
                                ID = Convert.ToInt32(leitor["ID_CONVIDADO"]),
                                Nome = leitor["CONVIDADO"].ToString(),
                            },
                            Evento = new EventosViewModel() {
                                ID = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Nome = leitor["EVENTO"].ToString(),
                                TipoEvento = new TiposEventosDomain() {
                                    Nome = leitor["TIPO_EVENTO"].ToString()
                                },
                                Local = leitor["LOCAL"].ToString(),
                                Data = Convert.ToDateTime(leitor["DATA_EVENTO"])
                            }
                        });
                    }
                    return lista;
                }
            }

            return null;
        }

        /// <summary>
        /// Lista todos os convites de um usuario
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <returns>Uma list acom todos os convites recebidos pela pessoa</returns>
        public List<ConvitesDomain> ListarMeusConvites(int ID) {

            using (SqlConnection connection = new SqlConnection()) {
                string comando = "SELECT * FROM VerConvites WHERE ID_CONVIDADE = @ID";
                SqlCommand cmd = new SqlCommand(comando, connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@ID",ID);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> lista = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        lista.Add(new ConvitesDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Usuario = new UsuariosViewModel() {
                                ID = Convert.ToInt32(leitor["ID_CONVIDADO"]),
                                Nome = leitor["CONVIDADO"].ToString(),
                            },
                            Evento = new EventosViewModel() {
                                ID = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Nome = leitor["EVENTO"].ToString(),
                                TipoEvento = new TiposEventosDomain() {
                                    Nome = leitor["TIPO_EVENTO"].ToString()
                                },
                                Local = leitor["LOCAL"].ToString(),
                                Data = Convert.ToDateTime(leitor["DATA_EVENTO"])
                            }
                        });
                    }
                    return lista;
                }
            }

            return null;
        }
    }
}
