using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Repositorios {
    /// <summary>
    /// Classe que lida com os dados inseridos atualizados e retornados na tabela eventos 
    /// </summary>
    public class EventosRepository : IEventosRepository {

        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;Integrated Security = true";

        /// <summary>
        /// Altera todos os valores de um evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento com o ID e com todas as informações ja alteradas</param>
        public void Alterar(EventosDomain evento) {
            if (evento.ID != 0) {
                using (SqlConnection conexao = new SqlConnection(Conexao)) {
                    string comando = "EXEC AlterarEvento @ID, @NOME , @DESCRICAO , @DATA_EVENTO , @ACESSO_LIVRE , @SITUACAO , @ID_INSTITUICAO , @ID_TIPO_EVENTO ";
                    conexao.Open();
                    SqlCommand cmd = new SqlCommand(comando, conexao);
                    cmd.Parameters.AddWithValue("@ID", evento.ID);
                    cmd.Parameters.AddWithValue("@NOME", evento.Nome);
                    cmd.Parameters.AddWithValue("@DESCRICAO", evento.Descricao);
                    cmd.Parameters.AddWithValue("@DATA_EVENTO", evento.DataEvento);
                    cmd.Parameters.AddWithValue("@ACESSO_LIVRE", evento.AcessoLivre);
                    cmd.Parameters.AddWithValue("@SITUACAO", evento.Situacao);
                    cmd.Parameters.AddWithValue("@ID_INSTITUICAO", evento.IDInstituicao);
                    cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO", evento.IDTipoEvento);

                    cmd.ExecuteNonQuery();
                }
            }
            throw new Exception("Não existe nenhum evento com este ID");
        }

        /// <summary>
        /// Cadastra um evento no banco de dados
        /// </summary>
        /// <param name="evento">Evento a ser cadastrado</param>
        public void Cadastrar(EventosDomain evento) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC CriarEvento @NOME , @DESCRICAO , @DATA_EVENTO , @ACESSO_LIVRE , @SITUACAO , @ID_INSTITUICAO , @ID_TIPO_EVENTO ";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME", evento.Nome);
                cmd.Parameters.AddWithValue("@DESCRICAO", evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO", evento.DataEvento);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE", evento.AcessoLivre);
                cmd.Parameters.AddWithValue("@SITUACAO", evento.Situacao);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO", evento.IDInstituicao);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO", evento.IDTipoEvento);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mostra todas as informações de todos os eventos cadastrados 
        /// </summary>
        /// <returns>Uma lista com todos os eventos do banco de dados</returns>
        public List<EventosDomain> Listar() {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM VerEventos";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        eventos.Add(
                            new EventosDomain() {
                                ID = Convert.ToInt32(leitor["EVENTO"]),
                                Nome = leitor["NOME_EVENTO"].ToString(),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                                AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                                Situacao = (EnSituacaoEvento)Convert.ToInt32(leitor["SITUACAO"]),
                                IDInstituicao = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Instituicao = new InstituicoesViewModel() {
                                    ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                    Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                    Logradouro = leitor["LOCAL"].ToString(),
                                    CEP = leitor["CEP"].ToString(),
                                    Cidade = leitor["CIDADE"].ToString(),
                                    UF = leitor["UF"].ToString()
                                },
                                IDTipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                TipoEvento = new TiposEventosDomain() {
                                    ID = Convert.ToInt32(leitor["TIPO_EVENTO"]),
                                    Nome = leitor["ID_TIPO_EVENTO"].ToString()
                                }
                            }
                        );
                    }
                    return eventos;
                }
            }
            throw new NullReferenceException("Não existe nenhum evento cadastrado no banco de dados");
        }

        /// <summary>
        /// Mostra todas as informações de um evento selecionado pelo ID
        /// </summary>
        /// <param name="ID">ID do evento selecionado</param>
        /// <returns>Retorna um evento com todas as informações</returns>
        public EventosDomain Listar(int ID) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM VerEventos WHERE ID = @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        return new EventosDomain() {
                            ID = Convert.ToInt32(leitor["EVENTO"]),
                            Nome = leitor["NOME_EVENTO"].ToString(),
                            Descricao = leitor["DESCRICAO"].ToString(),
                            DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                            AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                            Situacao = (EnSituacaoEvento)Convert.ToInt32(leitor["SITUACAO"]),
                            IDInstituicao = Convert.ToInt32(leitor["INSTITUICAO"]),
                            Instituicao = new InstituicoesViewModel() {
                                ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                Logradouro = leitor["LOCAL"].ToString(),
                                CEP = leitor["CEP"].ToString(),
                                Cidade = leitor["CIDADE"].ToString(),
                                UF = leitor["UF"].ToString()
                            },
                            IDTipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                            TipoEvento = new TiposEventosDomain() {
                                ID = Convert.ToInt32(leitor["TIPO_EVENTO"]),
                                Nome = leitor["ID_TIPO_EVENTO"].ToString()
                            }
                        };
                    }
                }
            }
            throw new NullReferenceException($"Não existe nenhum evento cadastrado com o id {ID} banco de dados");
        }

        /// <summary>
        /// Lista todos os eventos existentes em um intervalo de tempo
        /// </summary>
        /// <param name="dataInicial">Data inicial da procura. Não pode ser maior do que a dataFinal</param>
        /// <param name="dataFinal">Data final da procura. Não pode ser menor do que a dataInicial</param>
        /// <returns>Retorna uma lista com todos os eventos com</returns>
        public List<EventosDomain> Listar(DateTime dataInicial, DateTime dataFinal) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM VerEventos WHERE DATA_EVENTO > @DATA_INICIAL AND DATA_EVENTO < @DATA_FINAL";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@DATA_INICIAL", dataInicial.ToShortDateString());
                cmd.Parameters.AddWithValue("@DATA_FINAL", dataFinal.ToShortDateString());
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        eventos.Add(
                            new EventosDomain() {
                                ID = Convert.ToInt32(leitor["EVENTO"]),
                                Nome = leitor["NOME_EVENTO"].ToString(),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                                AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                                Situacao = (EnSituacaoEvento)Convert.ToInt32(leitor["SITUACAO"]),
                                IDInstituicao = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Instituicao = new InstituicoesViewModel() {
                                    ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                    Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                    Logradouro = leitor["LOCAL"].ToString(),
                                    CEP = leitor["CEP"].ToString(),
                                    Cidade = leitor["CIDADE"].ToString(),
                                    UF = leitor["UF"].ToString()
                                },
                                IDTipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                TipoEvento = new TiposEventosDomain() {
                                    ID = Convert.ToInt32(leitor["TIPO_EVENTO"]),
                                    Nome = leitor["ID_TIPO_EVENTO"].ToString()
                                }
                            }
                        );
                    }
                    return eventos;
                }
            }
            throw new NullReferenceException("Não há nenhum evento nesta data");
        }

        /// <summary>
        /// Busca todos os eventos do banco de dados que são de um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Tipo de evento que será filtrado</param>
        /// <returns>Uma lista de eventos com o mesmo tipo de evento</returns>
        public List<EventosDomain> Listar(TiposEventosDomain tipoEvento) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM VerEventos WHERE TIPO_EVENTO = @TIPO_EVENTO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@TIPO_EVENTO", tipoEvento.ID);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        eventos.Add(
                            new EventosDomain() {
                                ID = Convert.ToInt32(leitor["EVENTO"]),
                                Nome = leitor["NOME_EVENTO"].ToString(),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                                AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                                Situacao = (EnSituacaoEvento)Convert.ToInt32(leitor["SITUACAO"]),
                                IDInstituicao = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Instituicao = new InstituicoesViewModel() {
                                    ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                    Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                    Logradouro = leitor["LOCAL"].ToString(),
                                    CEP = leitor["CEP"].ToString(),
                                    Cidade = leitor["CIDADE"].ToString(),
                                    UF = leitor["UF"].ToString()
                                },
                                IDTipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                TipoEvento = new TiposEventosDomain() {
                                    ID = Convert.ToInt32(leitor["TIPO_EVENTO"]),
                                    Nome = leitor["ID_TIPO_EVENTO"].ToString()
                                }
                            }
                        );
                    }
                    return eventos;
                }
            }
            throw new NullReferenceException("Não há nenhum evento nesta data");
        }

        /// <summary>
        /// Busca todos os eventos do banco de dados de uma instituição
        /// </summary>
        /// <param name="instituicao">Instituicao que sera procurada</param>
        /// <returns>Uma lista com todos os eventos dessa instituição</returns>
        public List<EventosDomain> Listar(InstituicoesViewModel instituicao) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM VerEventos WHERE INSTITUICAO = @INSTITUICAO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@INSTITUICAO", instituicao.ID);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<EventosDomain> eventos = new List<EventosDomain>();
                    while (leitor.Read()) {
                        eventos.Add(
                            new EventosDomain() {
                                ID = Convert.ToInt32(leitor["EVENTO"]),
                                Nome = leitor["NOME_EVENTO"].ToString(),
                                Descricao = leitor["DESCRICAO"].ToString(),
                                DataEvento = Convert.ToDateTime(leitor["DATA_EVENTO"]),
                                AcessoLivre = Convert.ToBoolean(leitor["ACESSO_LIVRE"]),
                                Situacao = (EnSituacaoEvento)Convert.ToInt32(leitor["SITUACAO"]),
                                IDInstituicao = Convert.ToInt32(leitor["INSTITUICAO"]),
                                Instituicao = new InstituicoesViewModel() {
                                    ID = Convert.ToInt32(leitor["INSTITUICAO"]),
                                    Nome = leitor["NOME_INSTITUICAO"].ToString(),
                                    Logradouro = leitor["LOCAL"].ToString(),
                                    CEP = leitor["CEP"].ToString(),
                                    Cidade = leitor["CIDADE"].ToString(),
                                    UF = leitor["UF"].ToString()
                                },
                                IDTipoEvento = Convert.ToInt32(leitor["ID_TIPO_EVENTO"]),
                                TipoEvento = new TiposEventosDomain() {
                                    ID = Convert.ToInt32(leitor["TIPO_EVENTO"]),
                                    Nome = leitor["ID_TIPO_EVENTO"].ToString()
                                }
                            }
                        );
                    }
                    return eventos;
                }
            }
            throw new NullReferenceException("Não há nenhum evento nesta instituição");
        }
    }
}
