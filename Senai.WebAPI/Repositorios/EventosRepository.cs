using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class EventosRepository : IEventosRepository {

        private const string conexao = "Data Source = .\\SQLEXPRESS; initial catalog = SENAI_SVIGUFO_MANHA; user id = sa ; pwd = 132";

        public void Alterar(EventosDomain evento)
        {
            throw new NotImplementedException();
        }

        public EventosDomain BuscarPorID()
        {
            throw new NotImplementedException();
        }

        public void Cancelar(int ID)
        {
            throw new NotImplementedException();
        }

        public void Inserir(EventosDomain evento)
        {
            throw new NotImplementedException();
        }

        public List<EventosDomain> Listar()
        {
            List<EventosDomain> lista = new List<EventosDomain>();

            using(SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerInstituicoes";

                SqlCommand cmd = new SqlCommand(comando,connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (!leitor.HasRows)// verifica se não há registros 
                // se não ouver registros , retornara null
                    return null;

                while (leitor.Read()) {
                    lista.Add(
                        new EventosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                            AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                            Cancelado = Convert.ToBoolean(leitor["CANCELADO"]),
                            Instituicao = Convert.ToInt32(leitor["ID_INSTITUICAO"]),
                            TipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"])
                        }
                    );
                }
            }
    
            return lista;
        }
        /// <summary>
        /// Lista os eventos que estão nessa data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<EventosDomain> ListarPorData(DateTime data)
        {
            List<EventosDomain> lista = new List<EventosDomain>();

            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerInstituicoes WHERE DATA_EVENTO = @DATA_EVENTO";
                SqlCommand cmd = new SqlCommand(comando,connection);
                cmd.Parameters.AddWithValue("@DATA_EVENTO",data);
                SqlDataReader leitor = cmd.ExecuteReader();
                if(!leitor.HasRows)
                    return null;

                while (leitor.Read()) {
                    lista.Add(
                        new EventosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                            AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                            Cancelado = Convert.ToBoolean(leitor["CANCELADO"]),
                            Instituicao = Convert.ToInt32(leitor["ID_INSTITUICAO"]),
                            TipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"])
                        }
                    );
                }
            }

            return lista;
        }
    }
}
