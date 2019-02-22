using Senai.WebAPI.Enums;
    
namespace Senai.WebAPI.Domains {
    public class ConvitesDomain {
        public int ID;

        public int UsuarioID;
        public UsuariosDomain Usuario;

        public int EventoID;
        public EventosDomain Evento;
        
        public EnSituacaoConvite Status;
    }
}
