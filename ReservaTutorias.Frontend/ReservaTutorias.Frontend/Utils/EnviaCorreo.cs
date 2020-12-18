using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Utils
{
    public class EnviaCorreo : IEnviaCorreo
    {
        private readonly IConfiguration _config;
        public EnviaCorreo(IConfiguration config)
        {
            _config = config;
        }
        public bool EnviarCorreo(string destinatario, string tema, string mensaje)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config["CorreoInfo:Host"]));
                email.To.Add(MailboxAddress.Parse(destinatario));
                email.Subject = tema;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mensaje };
                using var smtp = new SmtpClient();
                smtp.Connect(_config["CorreoInfo:Provider"], Convert.ToInt32(_config["CorreoInfo:Puerto"]), MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_config["CorreoInfo:Host"], _config["CorreoInfo:Clave"]);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
