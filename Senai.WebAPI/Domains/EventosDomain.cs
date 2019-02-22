using Senai.WebAPI.ViewModels;
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
        public DateTime DataEvento { get; set; }  = DateTime.Now.Add(TimeSpan.FromDays(1)) ;

        
        public bool AcessoLivre { get ;set; }
        public bool Cancelado { get; set; }//não sei se é necessario ter essa propriedade
        /* 
            NA MANEIRA 3 : VOCÊ CRIA UMA VIEWMODEL
            LÁ TERÁ TODOS OS CAMPOS NECESSARIOS VOCÊ MANDARIA
        */
        public InstituicoesViewModel Instituicao { get; set; }
        public TiposEventosViewModel TipoEvento { get; set; }
    }
}
