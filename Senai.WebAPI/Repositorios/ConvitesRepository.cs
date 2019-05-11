using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class ConvitesRepository : IConvitesRepository {

        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;Integrated Security = true";

        /// <summary>
        /// Altera todos os valores de um convite no banco de dados
        /// </summary>
        /// <param name="convite">Convite a ser alterado</param>
        public void Alterar(ConvitesDomain convite) {
            using(SqlConnection connection = new SqlConnection(Conexao)) {
                string comando = "EXEC AlterarConvite @ID, @ID_USUARIO , @ID_EVENTO, @SITUACAO , @PALESTRANTE";
                connection.Open();
                SqlCommand cmd = new SqlCommand(comando,connection);
                cmd.Parameters.AddWithValue("@ID",convite.ID);
                cmd.Parameters.AddWithValue("@ID_USUARIO",convite.IDUsuario);
                cmd.Parameters.AddWithValue("@ID_EVENTO",convite.IDEvento);
                cmd.Parameters.AddWithValue("@SITUACAO",convite.Status);
                cmd.Parameters.AddWithValue("@PALESTRANTE",convite.Palestrante);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra um convite no banco de dados
        /// </summary>
        /// <param name="convite">Convite a ser cadastrado</param>
        public void Cadastrar(ConvitesDomain convite) {
            using (SqlConnection connection = new SqlConnection(Conexao)) {
                string comando = "EXEC InserirConvite @ID, @ID_USUARIO , @ID_EVENTO, @SITUACAO , @PALESTRANTE";
                connection.Open();
                SqlCommand cmd = new SqlCommand(comando, connection);
                cmd.Parameters.AddWithValue("@ID_USUARIO", convite.IDUsuario);
                cmd.Parameters.AddWithValue("@ID_EVENTO", convite.IDEvento);
                cmd.Parameters.AddWithValue("@SITUACAO", convite.Status);
                cmd.Parameters.AddWithValue("@PALESTRANTE", convite.Palestrante);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista todos os convites do banco de dados (ordenados por data (recente -> antigo))
        /// </summary>
        /// <returns>Uma lista com todos os convites do banco de dados</returns>
        public List<ConvitesDomain> Listar() {
            using(SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM CONVITES";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando,conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List <ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Não existe convites no banco de dados");
        }

        /// <summary>
        /// Retorna todas as informações de um convite
        /// </summary>
        /// <param name="ID">ID do convite</param>
        /// <returns>Um convite no ID selecionado , se não existir nenhum , retorna uma NullReferenceException</returns>
        public ConvitesDomain Listar(int ID) {
                using (SqlConnection conexao = new SqlConnection(Conexao)) {
                    string comando = "EXEC VerConvite @ID";
                    conexao.Open();
                    SqlCommand cmd = new SqlCommand(comando, conexao);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader leitor = cmd.ExecuteReader();

                    if (leitor.HasRows) {
                        while (leitor.Read()) {
                            return new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            };
                        }
                    }
                }

                throw new NullReferenceException("Não Existe convite com este ID");
        }

        /// <summary>
        /// Lista uma certa quantidade de convites
        /// </summary>
        /// <param name="pagina">Quantos registros serão pulados</param>
        /// <param name="quantidade">Quantidade de registros que será retornado</param>
        /// <returns>Retorna uma quantidade de convites a partir de um certo registro</returns>
        public List<ConvitesDomain> Listar(int pagina, int quantidade) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerTodosConvites @PAGINA , @QUANTIDADE";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@PAGINA",pagina);
                cmd.Parameters.AddWithValue("@QUANTIDADE", quantidade);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Não existe Convites nesta pagina");
        }

        public List<ConvitesDomain> Listar(int pagina, int quant, EnSituacaoConvite situacao) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos os convidados de um evento
        /// </summary>
        /// <param name="ID">ID do evento</param>
        /// <returns>Uma lista com todos os convidados do evento</returns>
        public List<ConvitesDomain> ListarConvidados(int ID) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerTodosConvidados @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Não há nenhuam pessoa convidada");
        }

        /// <summary>
        /// Lista todos os convidades de um determinado evento
        /// </summary>
        /// <param name="ID">ID do evento</param>
        /// <param name="pagina">A partir de qual convite será retornado</param>
        /// <param name="quantidade">Quantos convites serão retornados</param>
        /// <returns>Uma lista com todos os convidados de um determinado evento</returns>
        public List<ConvitesDomain> ListarConvidados(int ID, int pagina, int quantidade) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerConvidados @ID , @PAGINA , @QUANTIDADE";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@PAGINA", pagina);
                cmd.Parameters.AddWithValue("@QUANTIDADE", quantidade);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Não há convidados");
        }

        /// <summary>
        /// Lista uma certa quantidade de convites de um usuario
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <param name="pagina">quantidade de registos que serão pulados</param>
        /// <param name="quant">quantidade de registros que serão retornados (apos os ignorados)</param>
        /// <returns>Uma lista com todos os convites deste usuario</returns>
        public List<ConvitesDomain> MeusConvites(int ID, int pagina, int quant) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerMeusConvites @ID , @PAGINA , @QUANTIDADE";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@PAGINA", pagina);
                cmd.Parameters.AddWithValue("@QUANTIDADE", quant);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Nenhum convite encontrado");
        }

        /// <summary>
        /// Lista uma quantidade de convites com uma mesma situação de um certo usuario  
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <param name="pagina">pagina de procura</param>
        /// <param name="quant">quantidade de convites a serem retornados</param>
        /// <param name="situacao">situação dos convites procurados</param>
        /// <returns>Uma lista com uma quantidade de convites</returns>
        public List<ConvitesDomain> MeusConvites(int ID, int pagina, int quant, EnSituacaoConvite situacao) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerTodosConvites @PAGINA , @QUANTIDADE , @SITUACAO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@PAGINA", pagina);
                cmd.Parameters.AddWithValue("@QUANTIDADE", quant);
                cmd.Parameters.AddWithValue("@SITUACAO", (int)situacao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Usuario = new UsuariosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }
            throw new NullReferenceException("Nenhum convite encontrado");
        }

        /// <summary>
        /// Seleciona uam quantidade de eventos Palestradas de um Usuario
        /// </summary>
        /// <param name="ID">ID do usuario</param>
        /// <param name="pagina">quantidade de registos que serão pulados</param>
        /// <param name="quant">quantidade de registros que serão retornados (apos os ignorados)</param>
        /// <returns>Uma lista com todos os eventos palestrados Pelo usuario</returns>
        public List<ConvitesDomain> MinhasPalestras(int ID, int pagina, int quant) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerMinhasPalestras @ID , @PAGINA , @QUANTIDADE";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@PAGINA", pagina);
                cmd.Parameters.AddWithValue("@QUANTIDADE", quant);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<ConvitesDomain> Convites = new List<ConvitesDomain>();
                    while (leitor.Read()) {
                        Convites.Add(
                            new ConvitesDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                IDEvento = Convert.ToInt32(leitor["ID_EVENTO"]),
                                Evento = new EventosRepository().Listar(Convert.ToInt32(leitor["ID_EVENTO"])),
                                IDUsuario = Convert.ToInt32(leitor["ID_USUARIO"]),
                                Status = (EnSituacaoConvite)Convert.ToInt32(leitor["SITUACAO"]),
                                Palestrante = Convert.ToBoolean(leitor["PALESTRANTE"])
                            }
                        );
                    }
                    return Convites;
                }
            }

            throw new NullReferenceException("Nenhum convite encontrado");
        }
    }
}
