namespace ControleDeContatos.Helper
{
    public interface IEnviaEmail
    {
        bool Enviar(string destinatario, string assunto, string mensagem );
    }
}
