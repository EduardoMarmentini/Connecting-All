using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace ControleDeContatos.Helper
{
    public class EnviaEmail : IEnviaEmail
    {
        private readonly IConfiguration _config;

        public EnviaEmail(IConfiguration config)
        {
            _config = config;
        }

        public bool Enviar(string destinatario, string assunto, string mensagem)
        {
            try 
            {
                string host      = _config.GetValue<string>("SMTP:Host");
                string nome      = _config.GetValue<string>("SMTP:Nome");
                string username  = _config.GetValue<string>("SMTP:UserName");
                string senha     = _config.GetValue<string>("SMTP:Senha");
                int port         = _config.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(destinatario);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.UseDefaultCredentials = true;
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
            } 
            catch (Exception error )
            {
                Console.WriteLine($"Erro ao enviar e-mail: {error.Message}");
                return false;
            }
        }
    }
}
