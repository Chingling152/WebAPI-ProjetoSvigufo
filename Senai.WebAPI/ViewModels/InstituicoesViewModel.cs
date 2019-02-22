using System.ComponentModel.DataAnnotations;

namespace Senai.WebAPI.ViewModels {
    public class InstituicoesViewModel {
        [Required(ErrorMessage = "O ID É obrigatorio")]
        public int ID;
        public string Nome;
    }
}
