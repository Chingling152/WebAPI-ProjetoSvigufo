using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios 
{
    /// <summary>
    /// Classe que lida com a inserção , alteração e listagem de usuarios no banco de dados
    /// </summary>
    public class UsuariosRepository : IUsuariosRepository {

        /// <summary>
        /// string usada para se comunicar com o banco de dado.    
        /// Data source = local onde esta o banco de dados . 
        /// initial catalog = Banco de dado que será usado . 
        /// user = nome do usuario . 
        /// pwd = senha do usuario . 
        /// </summary>
        private const string conexao = "Data Source= .\\SQLEXPRESS ; initial catalog = SENAI_SVIGUFO_MANHA; user id = sa; pwd = 132";

        /// <summary>
        /// Altera todas as informações de um usuario (Não altera o tipo de usuario)
        /// </summary>
        /// <param name="usuario">Usuario com seus novos valores</param>
        public void Alterar(UsuariosDomain usuario) {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "UPDATE FROM USUARIOS SET NOME = @NOME,EMAIL = @EMAIL,SENHA = @SENHA WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(comando,connection);

                cmd.Parameters.AddWithValue("@NOME",usuario.Nome);
                cmd.Parameters.AddWithValue("@EMAIL", usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                cmd.Parameters.AddWithValue("@ID", usuario.ID);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Busca um usuario no banco de dados que tenha a combinação correta de email e senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public UsuariosDomain BuscarPorEmailSenha(string email, string senha) {
            using(SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
                SqlCommand cmd = new SqlCommand(comando,connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@EMAIL",email);
                cmd.Parameters.AddWithValue("@SENHA",senha);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new UsuariosDomain {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = leitor["ID_TIPO_USUARIO"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">O Usuario que sera cadastrado</param>
        public void Cadastrar(UsuariosDomain usuario) {
            using(SqlConnection connection = new SqlConnection(conexao)){
                string comando = "INSERT INTO USUARIOS(NOME,EMAIL,SENHA,ID_TIPO_USUARIO) VALUES(@NOME,@EMAIL,@SENHA,@ID_TIPO_USUARIO)";
                connection.Open();

                SqlCommand cmd = new SqlCommand(comando,connection);
                    cmd.Parameters.AddWithValue("@NOME",usuario.Nome);
                    cmd.Parameters.AddWithValue("@EMAIL",usuario.Email);
                    cmd.Parameters.AddWithValue("@SENHA",usuario.Senha);
                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO",usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// Mostra todos os usuarios cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public List<UsuariosDomain> Listar() {
            using (SqlConnection connection = new SqlConnection(conexao)) {
                string comando = "SELECT * FROM VerUsuarios";
                SqlCommand cmd = new SqlCommand(comando,connection);
                connection.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<UsuariosDomain> usuarios = new List<UsuariosDomain>();
                    while (leitor.Read()) {
                        usuarios.Add(
                            new UsuariosDomain() {
                                ID = Convert.ToInt32(leitor["ID"]),
                                Nome = leitor["NOME"].ToString(),
                                Email = leitor["EMAIL"].ToString(),
                                Senha = leitor["SENHA"].ToString(),
                                TipoUsuario = leitor["TIPO_USUARIO"].ToString()
                            }
                        );
                    }
                }

            }
            return null;
        }
    }
}