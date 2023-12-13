using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Digite o email")]
        public required string Email { get; set; }
    }
}
