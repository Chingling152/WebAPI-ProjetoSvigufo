using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class UsuariosRepository : IUsuariosRepository {
        private const string conexao = "Data Source= .\\SQLEXPRESS ; initial catalog = SENAI_SVIGUFO_MANHA; user id = sa; pwd = 132";

        public void Alterar(UsuariosDomain usuario) {
            throw new System.NotImplementedException();
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
                            TipoUsuario = Convert.ToInt32(leitor["ID_TIPO_USUARIO"])
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
            throw new System.NotImplementedException();
        }

        public bool Remover(UsuariosDomain usuario) {
            throw new System.NotImplementedException();
        }
    }
}