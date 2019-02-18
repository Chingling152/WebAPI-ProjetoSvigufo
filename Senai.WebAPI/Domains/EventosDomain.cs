using System;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class EventosDomain {
        public int ID;

        [Required(ErrorMessage ="O nome do evento não pode ser nulo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira uma descrição")]
        public string Descricao { get; set; }

        //data evento terá um valor padrão (?)
        public DateTime DataEvento { get; set; }  = DateTime.Now.Add(TimeSpan.FromDays(1)) ;

        //???
        public bool AcessoLivre { get ;set; }
        public bool Cancelado { get; set; }//não sei se é necessario ter essa propriedade

        [Required(ErrorMessage = "Você precisa inserir o ID da Instituição")]
        public int Instituicao { get; set; }

        [Required(ErrorMessage = "Você precisa inserir o ID de um Tipo de Evento")]
        public int TipoEvento { get; set; }

    }
}
