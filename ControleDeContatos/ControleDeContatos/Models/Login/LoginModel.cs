using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public required string Password { get; set; }
    }
}
