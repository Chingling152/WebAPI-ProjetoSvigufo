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
            NA MANEIRA 1 : VOCÊ MANDA O OBJETO INTEIRO COMO PARAMETRO
            LÁ TERÁ TODOS OS CAMPOS ENTÃO VOCÊ MANDARIA MUITAINFORMAÇÃO PARA APENAS USAR POUCAS
        */
        public InstituicoesDomain Instituicao { get; set; }
        public TiposEventosDomain TipoEvento { get; set; }
    }
}
