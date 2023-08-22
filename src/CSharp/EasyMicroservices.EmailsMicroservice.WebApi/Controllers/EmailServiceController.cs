using Microsoft.AspNetCore.Mvc;
using EasyMicroservices.EmailsMicroservice.Helpers;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using System.Net.Mail;
using System.Net;
using EasyMicroservices.EmailsMicroservice.DataTypes;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailServiceController
    {
            private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _emaillogic;
            private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _sendemaillogic;
            private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
            public EmailServiceController(IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> sendemaillogic, IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> emaillogic, IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic)
            {
                _emaillogic = emaillogic;
                _emailserverlogic = emailserverlogic;
                _sendemaillogic = sendemaillogic;
            }
        [HttpPost]
        public async Task SendEmail(SendMailContract request)
        {
            var Emails = await _emaillogic.GetAll();
            var Email = Emails.Result.FirstOrDefault();
            var SendEmail = await _sendemaillogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long>() { Id = Email.SendEmailId });
            var EmailServer = await _emailserverlogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long>() { Id = SendEmail.Result.EmailServerId });
            // Configure SMTP client settings
            var smtpClient = new SmtpClient
                {
                    Host = EmailServer.Result.Address,          // Your SMTP server
                    Port = EmailServer.Result.Port,                         // Port number
                    EnableSsl = false,                   // Use SSL
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(EmailServer.Result.Username, EmailServer.Result.Password) // Your SMTP credentials
                };

            // Create the email message
            var message = new MailMessage
                {
                    From = new MailAddress(Email.Address), // Sender's email address
                    Subject = request.Subject,
                    Body = request.Body,
                    IsBodyHtml = false, // You can set this to false if you're sending plain text
                };
            message.To.Add(request.EmailAdress); // Recipient's email address

            try
            {
                await smtpClient.SendMailAsync(message);
                await _sendemaillogic.Update(new UpdateSendEmailRequestContract()
                {
                    Id = SendEmail.Result.Id,
                    Status = EmailStatusType.Sent,
                    EmailServerId = SendEmail.Result.EmailServerId,
                    UniqueIdentity = SendEmail.Result.UniqueIdentity
                });
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                await _sendemaillogic.Update(new UpdateSendEmailRequestContract()
                {
                    Id = SendEmail.Result.Id,
                    Status = EmailStatusType.Canceled,
                    EmailServerId = SendEmail.Result.EmailServerId,
                    UniqueIdentity = SendEmail.Result.UniqueIdentity
                });
            }
        }
    }
}

