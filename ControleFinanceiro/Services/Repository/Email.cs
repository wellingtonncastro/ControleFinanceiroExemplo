using ControleFinanceiro.Models.Email;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Repository
{
    public class Email: IEmail
    {
        private ConfiguracaoEmail _emailConfiguration;
        public Email(IOptions<ConfiguracaoEmail> configuration)
        {
            _emailConfiguration = configuration.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var destiny = String.IsNullOrEmpty(email) ? _emailConfiguration.Email : email;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfiguration.Email, "Suporte")
            };
            mailMessage.To.Add(new MailAddress(destiny));
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            using (SmtpClient smtpClient = new SmtpClient(_emailConfiguration.Endereco, _emailConfiguration.Porta))
            {
                smtpClient.Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.senhaAcesso);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
