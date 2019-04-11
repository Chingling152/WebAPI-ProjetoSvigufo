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
                string comando = "AlterarConvite @ID, @ID_USUARIO , @ID_EVENTO, @SITUACAO , @PALESTRANTE";
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
                string comando = "InserirConvite @ID, @ID_USUARIO , @ID_EVENTO, @SITUACAO , @PALESTRANTE";
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
        /// Lista todos os convites do banco de dados
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
        /// Lista todos os convites de um determinado usuario
        /// </summary>
        /// <param name="ID">ID do usuario selecionado</param>
        /// <returns>Uma lista com todos os convites de um usuario</returns>
        public List<ConvitesDomain> MeusConvites(int ID) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM CONVITES WHERE ID = @ID";
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

            throw new NullReferenceException("Você não tem nenhum convite");
        }
    }
}
