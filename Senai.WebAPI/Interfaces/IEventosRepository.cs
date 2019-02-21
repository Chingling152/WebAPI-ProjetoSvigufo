using System;
using System.Collections.Generic;
using Senai.WebAPI.Domains;

namespace Senai.WebAPI.Interfaces {
    public interface IEventosRepository {

        List<EventosDomain> Listar();
    
        List<EventosDomain> ListarPorData(DateTime data);

        void Inserir(EventosDomain evento);

        void Cancelar(int ID);

        void Alterar(EventosDomain evento);
    }
}
