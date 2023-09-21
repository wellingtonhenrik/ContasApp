using ContasApp.Messages.Models;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace ContasApp.Messages.Services
{
    public static class EmailMessageService
    {
        private static string _conta = "wellingtonhenrik13@gmail.com.br";
        private static string _senha = "N2g4****";
        private static string _smtp = "smtp.gmail.com";
        private static int _porta = 587;

        //Método para fazer o envio das mensagens
        public static void Send(EmailMessagemModel model)
        {
            //Preparando o conteudp do email que será enviado

            var mailMesssage = new MailMessage(_conta, model.EmailDestinatario);
            mailMesssage.Subject = model.Assunto;
            mailMesssage.Body = model.Corpo;

            //Enviando o email
            var smtpClient = new SmtpClient(_smtp,_porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMesssage);
        }

    }
}
