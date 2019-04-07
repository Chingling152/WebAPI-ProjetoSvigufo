using Senai.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class ConvitesDomain {
        public int ID { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="O Convite precisa ter um evento")]
        [Display(Name = "Evento", Description = "Evento qual a pessoa será convidada")]
        public int IDEvento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Convite precisa ter um usuario")]
        [Display(Name = "Usuario", Description = "Pessoa a ser convidada")]
        public int IDUsuario { get; set; }

        [Display(Name = "Palestrante", Description = "Verifica se a pessoa é um palestrante")]
        public bool Palestrante { get; set; }

        [Display(Name = "Situação do convite")]
        public EnSituacaoConvite Status { get; set; }

        public UsuariosDomain Usuario { get; set; }
        public EventosDomain Evento { get; set; }
    }
}
