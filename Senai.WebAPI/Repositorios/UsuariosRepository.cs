using System;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class UsuariosRepository : IUsuariosRepository {

        private const string StringConexao = "Data Source = .\\SQLEXPRESS; initial catalog = ; user id = sa ; user pwd = 132";

        /// <summary>
        /// Retorna um usuario que contenha a combinação correta de email e senha
        /// </summary>
        /// <param name="email">Email do usuario que será procurado</param>
        /// <param name="senha">Senha do usuario que será procurado</param>
        /// <returns>Retorna um usuario com o Email e Senha inserids, se não existir, retorna null</returns>
        public UsuariosDomain BuscarPorEmailSenha(string email, string senha) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "SELECT * FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@EMAIL",email);
                cmd.Parameters.AddWithValue("@SENHA",senha);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new UsuariosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = Convert.ToInt32(leitor["ID_TIPO_USUARIO"])
                        };
                    }
                }
                
            }
            return null;
        }

        /// <summary>
        /// Cadastra um novo usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario a ser cadastrado</param>
        public void Cadastrar(UsuariosDomain usuario) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "INSERT INTO USUARIOS VALUES(@NOME,@EMAIL,@SENHA,@ID_TIPO_USUARIO)";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME",usuario.Nome);
                cmd.Parameters.AddWithValue("@EMAIL",usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA",usuario.Senha);
                cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO",usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Altera os valores de um usuario 
        /// </summary>
        /// <param name="usuario">Usuario que será alterado já com os novos valores nele</param>
        public void Alterar(UsuariosDomain usuario) {
            using (SqlConnection conexao = new SqlConnection(StringConexao)) {
                string comando = "UPDATE USUARIOS SET NOME = @NOME , EMAIL = @EMAIL , SENHA = @SENHA WHERE ID = @ID";
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);

                cmd.Parameters.AddWithValue("@NOME",usuario.Nome);
                cmd.Parameters.AddWithValue("@EMAIL",usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA",usuario.Senha);
                cmd.Parameters.AddWithValue("@ID",usuario.ID);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
