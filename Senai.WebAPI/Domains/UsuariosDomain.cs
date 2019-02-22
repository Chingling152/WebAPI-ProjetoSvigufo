using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class UsuariosDomain {
        public int ID;

        [Required(ErrorMessage ="Você precisa inserir um nome")]
        [StringLength(maximumLength: 250,MinimumLength = 2,ErrorMessage = "O Nome inserido é muito grande ou muito pequeno")]
        public string Nome;

        [Required(ErrorMessage = "Você precisa inserir um email")]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "O email inserido é muito grande ou muito pequeno")]
        [DataType(DataType.EmailAddress,ErrorMessage = "O Valor inserido não é um email valido")]
        public string Email;

        [StringLength(maximumLength:250,MinimumLength = 8,ErrorMessage = "A Senha é muito grande ou muito pequena")]
        public string Senha;

        [Required(ErrorMessage ="Insira um tipo de usuario")]
        [StringLength(maximumLength: 59, MinimumLength = 3, ErrorMessage = "O tipo de usuario muito grande ou muito pequena")]
        public string TipoUsuario;
    }
}
