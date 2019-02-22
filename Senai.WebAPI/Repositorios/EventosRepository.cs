using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class EventosRepository : IEventosRepository {

        private const string StringConexao = "Data Source = .\\SQLEXPRESS; initial catalog = ; user id = sa ; user pwd = 132";

        public void Alterar(EventosDomain evento) {
            throw new NotImplementedException();
        }

        public void Cancelar(int ID) {
            throw new NotImplementedException();
        }

        public void Inserir(EventosDomain evento) {
            throw new NotImplementedException();
        }

        public List<EventosDomain> Listar() {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "SELECT * FROM VerEventos";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();
                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        eventos.Add(
                            new EventosDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                Nome = leitor["EVENTO"].ToString(),
                                TipoEvento = new TiposEventosDomain() {
                                    ID = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                    Nome = leitor["ID_TIPO_EVENTO"].ToString()
                                },
                                Instituicao = new InstituicoesDomain() {

                                }
                            }
                        );
                    }
                }
            }
            return null;
        }

        public List<EventosDomain> ListarPorData(DateTime data) {
            throw new NotImplementedException();
        }
    }
}
