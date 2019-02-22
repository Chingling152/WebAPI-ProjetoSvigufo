using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Repositorios {
    public class ConvitesRepository : IConvitesRepository {
        private const string Conexao = "Data Source=.\\SQLEXPRESS; initial catalog = SENAI_SVIGUFO_MANHA;user id = sa; pwd = 132";

        public void Alterar(ConvitesDomain convite) {
            throw new NotImplementedException();
        }

        public void Cadastrar(ConvitesDomain convite) {
            using (SqlConnection connection = new SqlConnection(Conexao)) {
                connection.Open();

                string comando = "INSERT INTO CONVITES(ID_USUARIO,ID_EVENTO) VALUES (@ID_USUARIO,@ID_EVENTO)";
                SqlCommand cmd = new SqlCommand(comando,connection);

                cmd.Parameters.AddWithValue("@ID_USUARIO",convite.Usuario.ID);
                cmd.Parameters.AddWithValue("@ID_EVENTO",convite.Evento.ID);

                cmd.ExecuteNonQuery();
            }
        }

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
