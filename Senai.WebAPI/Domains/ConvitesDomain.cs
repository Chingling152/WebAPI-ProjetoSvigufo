using Senai.WebAPI.Enums;
using Senai.WebAPI.ViewModels;
    
namespace Senai.WebAPI.Domains {
    public class ConvitesDomain {
        public int ID;
        public UsuariosViewModel Usuario;
        public EventosViewModel Evento;
        public EnSituacaoConvite Status;
    }
}
