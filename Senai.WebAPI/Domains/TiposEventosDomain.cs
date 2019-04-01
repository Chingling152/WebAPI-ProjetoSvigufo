using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class TiposEventosDomain
    {
        public int ID { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "O Nome só pode conter letras")]
        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(AllowEmptyStrings = false,ErrorMessage ="Você não pode criar um tipo de evento sem nome")]
        public string Nome { get; set; }
    }
}
