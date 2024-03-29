using System.Net;
using System.Net.Mail;


namespace ProjetoProdutos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuracao;

        public Email(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = "smtp.gmail.com";
                string emailRemetente = Environment.GetEnvironmentVariable("SMTP_EMAILREMETENTE"); 
                string senhaRemetente = "drxuudvgjchcojep";
                int porta = 587;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(emailRemetente)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailRemetente, senhaRemetente);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
