using System;
using System.ComponentModel.DataAnnotations;
using Senai.WebAPI.Enums;
using Senai.WebAPI.ViewModels;

namespace Senai.WebAPI.Domains {
    public class EventosDomain {

        public int ID;

        [Required(AllowEmptyStrings = false, ErrorMessage ="O nome do evento não pode ser nulo")]
        [StringLength(maximumLength:250,MinimumLength = 3,ErrorMessage ="O nome inserido é invalido")]
        [Display(Name = "Titulo do Evento")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Insira uma descrição")]
        [StringLength(maximumLength:400,MinimumLength =0,ErrorMessage ="A Descrição inserida é muito grande")]
        [Display(Name = "Descrição do evento")]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="O Evento precisa de uma data")]
        [DataType(DataType.DateTime,ErrorMessage ="O Valor inserido não é uma data")]
        [Display(Name = "Data do evento", Description = "Data de quando ocorrerá o evento")]
        public DateTime DataEvento { get; set; }

        [Display(Name = "Acesso Livre",Description ="Se o evento será aberto ao publico ou não")]
        public bool AcessoLivre { get ;set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa de uma instituição")]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "A instituição precisa ser referenciada pelo ID")]
        [Display(Name ="Instituição",Description ="Instituição onde acontecerá o evento")]
        public int IDInstituicao { get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa de um tipo de evento")]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "O Tipo de evento precisa ser referenciado pelo ID")]
        [Display(Name = "Tipo de evento",Description ="Sobre o que o evento é focado")]
        public int IDTipoEvento { get; set;}

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Evento precisa ter uma situação")]
        [Display(Name = "Situação do evento")]
        public EnSituacaoEvento Situacao { get;set;}

        public InstituicoesViewModel Instituicao { get; set; }
        public TiposEventosDomain TipoEvento { get; set; }
    }
}
