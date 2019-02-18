using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class InstituicoesDomain
    {
        public int ID;

        [Required(ErrorMessage = "Nome Fantasia não pode ser nulo")]
        public string NomeFantasia;

        [Required(ErrorMessage = "Razão Social não pode ser nulo")]
        public string RazaoSocial;

        [Required(ErrorMessage = "O Logradouro não pode ser nulo")]
        public string Logradouro;

        [Required(ErrorMessage = "O CEP é obrigatorio")]
        public string CEP;

        [Required(ErrorMessage = "O CNPJ não pode ser nulo")]
        public string CNPJ;

        [StringLength(2,MinimumLength = 2,ErrorMessage ="A UF Deve conter exatos 2 Caracteres")]
        [Required(ErrorMessage = "A UF não pode ser nulo")]
        public string UF;

        [Required(ErrorMessage = "A Cidade é obrigatoria")]
        public string Cidade;
        
    }
}
