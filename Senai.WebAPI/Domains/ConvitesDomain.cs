using Senai.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class ConvitesDomain {
        public int ID { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="O Convite precisa ter um evento")]
        public int IDEvento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O Convite precisa ter um usuario")]
        public int IDUsuario { get; set; }

        public UsuariosDomain Usuario { get; set; }
        public EventosDomain Evento { get; set; }
        public EnSituacaoConvite Status { get; set; }
    }
}
