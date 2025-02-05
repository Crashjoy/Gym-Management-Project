using GymManagement.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit.Text;
using MimeKit;

namespace GymManagement.Utilities
{
    public class EmailService
    {
        /// <summary>
        /// This implements the IEmailService from
        /// Microsoft.AspNetCore.Identity.UI.Services for the Identity System
        /// </summary>
        public class EmailSender : IEmailSender
        {
            private readonly IEmailConfiguration _emailConfiguration;
            private readonly ILogger<EmailSender> _logger;

            public EmailSender(IEmailConfiguration emailConfiguration, ILogger<EmailSender> logger)
            {
                _emailConfiguration = emailConfiguration;
                _logger = logger;
            }

            /// <summary>
            /// Asynchronously builds and sends a message to a single recipient.
            /// </summary>
            /// <param name="email"></param>
            /// <param name="subject"></param>
            /// <param name="htmlMessage"></param>
            /// <returns></returns>
            public async Task SendEmailAsync(string email, string subject, string htmlMessage)
            {
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(email, email));
                message.From.Add(new MailboxAddress(_emailConfiguration.SmtpFromName, _emailConfiguration.SmtpUsername));

                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = htmlMessage
                };

                await SendEmailMessageAsync(message);

            }

            /// <summary>
            /// Sends the MimeMessage
            /// </summary>
            /// <param name="theMessage">The MimeMessage</param>
            /// <returns></returns>
            public async Task SendEmailMessageAsync(MimeMessage theMessage)
            {
                try
                {
                    using var emailClient = new SmtpClient();

                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);


                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    await emailClient.SendAsync(theMessage);

                    emailClient.Disconnect(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.GetBaseException().Message);
                }
            }

        }

    }
}
