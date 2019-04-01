using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Views {
    public class LoginViewModel {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Insira um email para fazer login")]
        public string Email;
        public string Senha;
    }
}
