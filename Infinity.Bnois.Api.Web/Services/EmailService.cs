using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EmailService'
    public class EmailService : IIdentityMessageService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EmailService'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EmailService.SendAsync(IdentityMessage)'
        public Task SendAsync(IdentityMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EmailService.SendAsync(IdentityMessage)'
        {
            var fromAddress = new MailAddress("demobpsc@gmail.com", "Bangladesh Naval Officer Information System");
            var toAddress = new MailAddress(message.Destination);

            const string fromPassword = "bpsc@12345";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = message.Subject,
                Body = message.Body
            })
            {
                smtp.Send(mailMessage);
            }
            return Task.FromResult(0);
        }
    }
}