using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Notofication.Service.Email.ISvcs;
using Notofication.Service.Email.Models;
using Notofication.Service.RazorEngine.ISvcs;
using System.Text.RegularExpressions;
namespace Notofication.Service.Email.Svcs
{
    public class EmailSvc : IEmailSvc
    {
        private readonly ILogger _log;
        private readonly IRazorViewRenderer _razorOperation;
        private readonly IConfiguration _config;

        public EmailSvc(ILogger<EmailSvc> log, IRazorViewRenderer razorOperation, IConfiguration config)
        {
            _log = log;
            _config = config;
            _razorOperation = razorOperation;
        }

        public async Task<string> ConstructMailAsync<T>(T model)
        {
            try
            {
                var view = await _razorOperation.RenderViewAsync(typeof(T).Name + ".cshtml", model);
                return view;
            }
            catch (Exception ex)
            {
                _log.LogError("Something went wrong: {ex}", ex);
                throw;
            }
        }

        public async Task<EmailResponse> SendEmailAsync(EmailRequest email)
        {
            try
            {
                var message = new MimeMessage();
                string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

                message.From.Add(new MailboxAddress(_config["Email:Config:UserName"], _config["Email:Config:Address"]));



                message.To.Add(new MailboxAddress("Altrocks", email.To));

               

                message.Subject = email.Subject;
                message.Body = new TextPart("html")
                {
                    Text = email.Body
                };

                if (email.Attachment != null)
                {
                    foreach (var attachment in email.Attachment)
                    {
                        var multipart = new Multipart("mixed");
                        var attachmentPart = new MimePart
                        {
                            Content = new MimeContent(attachment.OpenReadStream(), ContentEncoding.Default),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = attachment.FileName
                        };

                        multipart.Add(attachmentPart);
                        multipart.Add(message.Body);
                        message.Body = multipart;
                    }
                }

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_config["Email:Config:Host"], Convert.ToInt32(_config["Email:Config:Port"]), SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_config["Email:Config:Address"], _config["Email:Config:Password"]);
               // await smtpClient.SendAsync(message);
                var response = await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);

                return new EmailResponse()
                {
                    Status = response.Contains("2.0.0 OK"),
                    Response = response
                };
            }
            catch (Exception ex)
            {
                _log.LogError("Something went wrong: {ex}", ex);
                throw;
            }
        }
    }

}
