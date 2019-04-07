using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Senai.WebAPI.Domains;
using Senai.WebAPI.Interfaces;

namespace Senai.WebAPI.Repositorios {
    public class ConvitesRepository : IConvitesRepository {

        private const string Conexao = "Data Source = .\\NOVOSERVIDOR; Initial Catalog = PROJETO_SVIGUFO;Integrated Security = true";

        public void Alterar(ConvitesDomain convite) {
            throw new System.NotImplementedException();
        }

        public void Cadastrar(ConvitesDomain convite) {
            throw new System.NotImplementedException();
        }

        public List<ConvitesDomain> Listar() {
            using(SqlConnection conexao = new SqlConnection()) {
                string Comando = "SELECT * FROM VerConvites";
            }

            throw new NullReferenceException("Não existe convites no banco de dados");
        }

        public List<ConvitesDomain> MeusConvites(int ID) {
            throw new System.NotImplementedException();
        }
    }
}
