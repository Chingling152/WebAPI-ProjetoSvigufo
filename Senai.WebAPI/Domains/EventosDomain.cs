using System;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class EventosDomain {

        public int ID;

        [Required(ErrorMessage ="O nome do evento não pode ser nulo")]
        [StringLength(maximumLength:250,MinimumLength = 3,ErrorMessage ="O nome inserido é invalido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira uma descrição")]
        public string Descricao { get; set; }

        //data evento terá um valor padrão (?)
        public DateTime DataEvento { get; set; }

        
        public bool AcessoLivre { get ;set; }

        /*
         * Maneira 2 : Cria variaveis para o ID 
         */
        public int InstituicaoID { get;set;}
        public int TipoEventoID {get;set;}

        public InstituicoesDomain Instituicao { get; set; }
        public TiposEventosDomain TipoEvento { get; set; }

    }
}
