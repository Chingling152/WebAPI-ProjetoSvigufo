using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class EventosRepository : IEventosRepository {

        private const string conexao = "Data Source=.\\SQLEXPRESS; initial catalog = SENAI_SVIGUFO_MANHA;user id = sa; pwd = 132";

        public void Alterar(EventosDomain evento) {
            throw new NotImplementedException();
        }
        public void Cancelar(int ID) {
            throw new NotImplementedException();
        }

        public void Inserir(EventosDomain evento) {
            using(SqlConnection connection = new SqlConnection(conexao)){
                string comando = "INSERT INTO EVENTOS(NOME,DESCRICAO,DATA_EVENTO,ID_INSTITUICAO,ID_TIPO_EVENTO,ACESSO_LIVRE,CANCELADO) VALUES(@NOME,@DESCRICAO,@DATA_EVENTO,@ID_INSTITUICAO,@ID_TIPO_EVENTO,@ACESSO_LIVRE,@CANCELADO)";
                SqlCommand cmd = new SqlCommand(comando,connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@NOME",evento.Nome);
                cmd.Parameters.AddWithValue("@DESCRICAO",evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO",evento.DataEvento);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO",evento.InstituicaoID);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO",evento.TipoEventoID);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE",evento.AcessoLivre);
                cmd.Parameters.AddWithValue("@CANCELADO",evento.Cancelado);

                cmd.ExecuteNonQuery();
            }
        }

        public List<EventosDomain> Listar() {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerEventos";
                connection.Open();

                SqlCommand cmd = new SqlCommand(comando,connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (!leitor.HasRows) {
                    return null;
                }
                List<EventosDomain> lista = new List<EventosDomain>();
                while (leitor.Read()) {
                    lista.Add(
                        new EventosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["EVENTO"].ToString(),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            //Criação do objeto TipoEvento
                            TipoEvento = new TiposEventosDomain() {
                                ID = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                Nome = leitor["TIPO_EVENTO"].ToString()
                            },
                            //Criação do objeto Instituição
                            Instituicao = new InstituicoesDomain(){
                                ID = Convert.ToInt32(leitor["ID_INSTITUICAO"]),
                                NomeFantasia = leitor["INSTITUICAO"].ToString(),
                            }
                        }
                    );
                }
                return lista;

            }
        }

        public List<EventosDomain> ListarPorData(DateTime data) {
            throw new NotImplementedException();
        }
    }
}
