using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class UsuariosRepository : IUsuariosRepository {
        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;user id = sa;pwd = 132";

        public void Alterar(UsuariosDomain usuario) {
            throw new System.NotImplementedException();
        }

        public UsuariosDomain Logar(string email, string senha) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Cadastra um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario que será cadastrado</param>
        public void Cadastrar(UsuariosDomain usuario) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "CriarUsuario @NOME , @EMAIL , @SENHA , @TIPO_USUARIO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                cmd.Parameters.AddWithValue("@EMAIL", usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO", (int)usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retorna todos os usuarios do banco de dados
        /// </summary>
        /// <returns>Uma lista com todos os usuarios do banco de dados</returns>
        public List<UsuariosDomain> Listar() {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM USUARIO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    List<UsuariosDomain> Usuarios = new List<UsuariosDomain>();
                    while (leitor.Read()) {
                        Usuarios.Add(
                          new UsuariosDomain() {
                              ID = Convert.ToInt32(leitor["ID"]),
                              Nome = leitor["NOME"].ToString(),
                              Email = leitor["EMAIL"].ToString(),
                              Senha = leitor["SENHA"].ToString(),
                              TipoUsuario = (EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                          }  
                        );
                    }
                    return Usuarios;
                }
            }
            throw new NullReferenceException("Não existe usuarios cadastrados no banco de dados");
        }

        public UsuariosDomain Listar(int ID) {
            throw new System.NotImplementedException();
        }
    }
}
