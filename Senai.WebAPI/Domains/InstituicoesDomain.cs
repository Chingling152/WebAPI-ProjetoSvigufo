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
        [Required(ErrorMessage = "Razão Social não pode ser nulo")]
        public string RazaoSocial;

        [StringLength(maximumLength: 300, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(ErrorMessage = "O Logradouro não pode ser nulo")]
        public string Logradouro;

        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "O CEP Deve conter exatos 8 Caracteres")]
        [Required(ErrorMessage = "O CEP é obrigatorio")]
        public string CEP;

        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "O CNPJ Deve conter exatos 14 Caracteres")]
        [Required(ErrorMessage = "O CNPJ não pode ser nulo")]
        public string CNPJ;

        [StringLength(maximumLength: 2,MinimumLength = 2,ErrorMessage ="A UF Deve conter exatos 2 Caracteres")]
        [Required(ErrorMessage = "A UF não pode ser nula")]
        public string UF;

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(ErrorMessage = "A Cidade é obrigatoria")]
        public string Cidade;
        
    }
}
