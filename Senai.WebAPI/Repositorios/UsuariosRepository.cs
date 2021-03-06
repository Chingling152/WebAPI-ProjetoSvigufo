﻿using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Enums;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class UsuariosRepository : IUsuariosRepository {
        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;Integrated Security = true";

        /// <summary>
        /// Altera todas as informações de um usuario no banco de dados
        /// </summary>
        /// <param name="usuario">Usuario a ser alterado (precisa ter o ID)</param>
        public void Alterar(UsuariosDomain usuario) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "AlterarUsuario @ID, @NOME , @EMAIL , @SENHA , @TIPO_USUARIO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", usuario.ID);
                cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                cmd.Parameters.AddWithValue("@EMAIL", usuario.Email);
                cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO", (int)usuario.TipoUsuario);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Procura um usuario com a combinação de email e senha inserido
        /// </summary>
        /// <param name="email">Email do usuario que será buscado</param>
        /// <param name="senha">Senha do usuario que será buscado</param>
        /// <returns>Retorna um usuario qu tenha a senha e o email igual oas inseridos nos parametros</returns>
        public UsuariosDomain Logar(string email, string senha) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@SENHA", senha);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new UsuariosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = (EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                        };
                    }
                }
            }
            throw new NullReferenceException("Email ou senha incorretos");
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
                string comando = "SELECT * FROM USUARIOS";
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

        /// <summary>
        /// Retorna um usuario especifico do banco de dados
        /// </summary>
        /// <param name="ID">ID do usuario selecionado</param>
        /// <returns>Retorna o usuario se ele existir , senão retorna uma NullReferenceException</returns>
        public UsuariosDomain Listar(int ID) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "SELECT * FROM USUARIOS WHERE ID = @ID";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@ID", ID);

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows) {
                    while (leitor.Read()) {
                        return new UsuariosDomain() {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Nome = leitor["NOME"].ToString(),
                            Email = leitor["EMAIL"].ToString(),
                            Senha = leitor["SENHA"].ToString(),
                            TipoUsuario = (EnTipoUsuario)Convert.ToInt32(leitor["TIPO_USUARIO"])
                        };
                    }
                }
            }
            throw new NullReferenceException("Não existe usuario no ID selecionado");
        }
        
        /// <summary>
        /// Filtra a lista de usuarios no banco de dados e retorna apenas com um tipo de usuario selecionado
        /// </summary>
        /// <param name="tipoUsuario">Tipo de usuario que todos devem ter</param>
        /// <returns>Retorna uma lista de usuarios com o mesmo nivel de privilegios</returns>
        public List<UsuariosDomain> Listar(EnTipoUsuario tipoUsuario) {
            using (SqlConnection conexao = new SqlConnection(Conexao)) {
                string comando = "EXEC VerUsuarios @TIPO_USUARIO";
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@TIPO_USUARIO",(int)tipoUsuario);
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
            throw new NullReferenceException($"Não existe nenhum usuario do tipo {tipoUsuario.ToString()}");
        }
    }
}
