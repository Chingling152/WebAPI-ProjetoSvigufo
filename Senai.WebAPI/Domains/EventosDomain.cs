using System;
using System.ComponentModel.DataAnnotations;
using Senai.WebAPI.Enums;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Domains {
    public class EventosDomain {

        public int ID;

        [Required(AllowEmptyStrings = false, ErrorMessage ="O nome do evento não pode ser nulo")]
        [StringLength(maximumLength:250,MinimumLength = 3,ErrorMessage ="O nome inserido é invalido")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Insira uma descrição")]
        [StringLength(maximumLength:400,MinimumLength =0,ErrorMessage ="A Descrição inserida é muito grande")]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="O Evento precisa de uma data")]
        [DataType(DataType.DateTime,ErrorMessage ="O Valor inserido não é uma data")]
        public DateTime DataEvento { get; set; }

        public bool AcessoLivre { get ;set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa de uma instituição")]
        public int IDInstituicao { get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa de um tipo de evento")]
        public int IDTipoEvento { get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa ter uma situação")]
        public EnSituacaoEvento Situacao { get;set;}

        public InstituicoesViewModel Instituicao { get; set; }
        public TiposEventosDomain TipoEvento { get; set; }
    }
}
