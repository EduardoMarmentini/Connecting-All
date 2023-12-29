namespace ControleDeContatos.Models.Login
{
    // Isso é uma ViewModel que te permite que uma view utilize mais de uma model para trabalhar
    public class LoginAndRecoverViewModel
    {
        // Login model para trabalhar na parte de entrada do usuario no sistema
        public LoginModel LoginModel { get; set; }
        // Redefinir senha model para trabalhar com a recuperação de senha do usuario
        public RedefinirSenhaModel RedefinirSenhaModel { get; set; }

    }
}
