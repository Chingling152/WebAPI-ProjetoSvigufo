using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class TiposEventosDomain
    {
        public int ID { get; set; }

        [RegularExpression("^[a-zA-Z ç ~ ã õ ê â î ô ñ û ú í á é ó ü ï ä ö ë]*$", ErrorMessage = "O Nome só pode conter letras")]
        [StringLength(maximumLength: 250, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(AllowEmptyStrings = false,ErrorMessage ="Você não pode criar um tipo de evento sem nome")]
        public string Nome { get; set; }
    }
}
