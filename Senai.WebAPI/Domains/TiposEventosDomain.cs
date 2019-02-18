using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.Domains
{
    public class TiposEventosDomain
    {
        public int ID { get; set; }

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "O numero de caracteres inseridos não é aceito")]
        [Required(ErrorMessage ="Você não pode criar um tipo de evento sem nome")]
        public string Nome { get; set; }
    }
}
