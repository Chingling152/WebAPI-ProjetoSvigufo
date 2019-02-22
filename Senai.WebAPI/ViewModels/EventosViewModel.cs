using System;
using Senai.WebAPI.Domains;

namespace Senai.WebAPI.ViewModels {
    public class EventosViewModel {
        public int ID;
        public string Nome;
        public string Descricao;
        public TiposEventosDomain TipoEvento;
        public DateTime Data;
        public string Local;
    }
}
