using Senai.WebAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains {
    public class UsuariosDomain {
        public int ID;

        [RegularExpression("^[a-zA-Z]*$",ErrorMessage = "O Nome do usuario só pode conter letras")]
        [Required(ErrorMessage = "Insira um nome", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 250,MinimumLength = 2,ErrorMessage = "O Nome inserido é muito grande ou muito pequeno")]
        public string Nome;

        [Required(ErrorMessage = "Insira um email", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "O email inserido é muito grande ou muito pequeno")]
        [EmailAddress(ErrorMessage = "O Valor inserido não é um email valido")]
        public string Email;

        [Required(ErrorMessage = "Insira uma senha",AllowEmptyStrings =false)]
        [StringLength(maximumLength:250,MinimumLength = 8,ErrorMessage = "A Senha é muito grande ou muito pequena")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "A senha deve conter numeros e letras")]
        public string Senha;

        [Required(ErrorMessage = "Insira um tipo de usuario", AllowEmptyStrings = false)]
        [Display(Name ="Tipo de usuario",Description ="Quantidade de privilegios de um Usuario")]
        public EnTipoUsuario TipoUsuario;
    }
}
