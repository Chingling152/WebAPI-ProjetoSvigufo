using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class InstituicoesDomain
    {
        public int ID;

        [Required(ErrorMessage = "Nome Fantasia não pode ser nulo")]
        [StringLength(maximumLength: 250,MinimumLength = 2,ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        public string NomeFantasia;

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Razão Social não pode ser nulo")]
        public string RazaoSocial;

        [StringLength(maximumLength: 300, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "O Logradouro não pode ser nulo")]
        public string Logradouro;

        [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP só pode conter numeros")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "O CEP Deve conter exatos 8 Caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O CEP é obrigatorio")]
        public string CEP;

        [RegularExpression("^[0-9]*$", ErrorMessage = "O CNPJ só pode conter numeros")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "O CNPJ Deve conter exatos 14 Caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O CNPJ não pode ser nulo")]
        public string CNPJ;

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "A UF só pode conter letras")]
        [StringLength(maximumLength: 2,MinimumLength = 2,ErrorMessage ="A UF Deve conter exatos 2 Caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A UF não pode ser nula")]
        public string UF;

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Cidade é obrigatoria")]
        public string Cidade;
        
    }
}
