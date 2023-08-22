using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using EasyMicroservices.ServiceContracts;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Helpers
{
    public class EmailManager
    {
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _emaillogic;
        private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _sendemaillogic;
        private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        public EmailManager(IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> sendemaillogic,IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> emaillogic ,IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic) 
        {
            _emaillogic = emaillogic;
            _emailserverlogic = emailserverlogic;
            _sendemaillogic = sendemaillogic;
        }

        public async Task SendEmailAsync(SendMailContract request)
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
                EnableSsl = EmailServer.Result.IsSSL,                   // Use SSL
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
                IsBodyHtml = true, // You can set this to false if you're sending plain text
            };


        }
    }
}

