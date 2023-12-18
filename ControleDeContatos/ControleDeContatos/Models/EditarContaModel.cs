using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class EditarContaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Informa o tipo do usuário")]
        public required string Login { get; set; }

        public string? New_Password { get; set; }

        public string? Confirm_New_Password { get; set; }
        public DateTime? DataAlteração { get; set; }

        public void SetSenhaHash()
        {
            New_Password = New_Password.GerarHash();
        }

        public bool ValidaNovaSenha()
        {
           return New_Password.ToUpper() == Confirm_New_Password.ToUpper();
        }

    }
}
