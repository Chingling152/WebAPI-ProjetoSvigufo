using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class InstituicoesDomain
    {
        public int ID;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome Fantasia não pode ser nulo")]
        [StringLength(maximumLength: 250,ErrorMessage = "O numero de caracteres inseridos não pode exceder 250")]
        [Display(Name = "Nome Fantasia",Description = "Nome fantasia da instituição")]
        public string NomeFantasia;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Razão Social não pode ser nulo")]
        [StringLength(maximumLength: 250, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Display(Name = "Razão Social", Description = "Nome de registro da instituição")]
        public string RazaoSocial;

        [Required(AllowEmptyStrings = false,ErrorMessage = "O Logradouro não pode ser nulo")]
        [StringLength(maximumLength: 300, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Display(Name = "Logradouro", Description = "Localização da instituição")]
        public string Logradouro;

        [Required(AllowEmptyStrings = false, ErrorMessage = "O CEP é obrigatorio")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "O CEP Deve conter exatos 8 Caracteres")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "O CEP só pode conter numeros")]
        public string CEP;

        [Required(AllowEmptyStrings = false, ErrorMessage = "O CNPJ não pode ser nulo")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "O CNPJ Deve conter exatos 14 Caracteres")]
        [RegularExpression(@"^[0-9]{14,14}$", ErrorMessage = "O CNPJ só pode conter numeros")]
        public string CNPJ;

        [Required(AllowEmptyStrings = false, ErrorMessage = "A UF não pode ser nula")]
        [StringLength(maximumLength: 2,MinimumLength = 2,ErrorMessage ="A UF Deve conter exatos 2 Caracteres")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "A UF só pode conter letras")]
        public string UF;

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Cidade é obrigatoria")]
        [StringLength(maximumLength: 250, ErrorMessage = "O numero de caracteres inseridos não pode exceder 250")]
        public string Cidade;
        
    }
}
