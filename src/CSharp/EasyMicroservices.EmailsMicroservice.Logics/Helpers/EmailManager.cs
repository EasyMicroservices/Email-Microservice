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
        private readonly IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> _QueueEmaillogic;
        private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        public EmailManager(IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> QueueEmaillogic,IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> emaillogic ,IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic) 
        {
            _emaillogic = emaillogic;
            _emailserverlogic = emailserverlogic;
            _QueueEmaillogic = QueueEmaillogic;
        }

        public async Task QueueEmail(SendMailContract request)
        {
            var Emails = await _emaillogic.GetAll();
            var Email = Emails.Result.FirstOrDefault();
            var QueueEmail = await _QueueEmaillogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long>() { Id = Email.QueueEmailId });
            var EmailServer = await _emailserverlogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long>() { Id = QueueEmail.Result.EmailServerId });
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
                await _QueueEmaillogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = QueueEmail.Result.Id,
                    Status = EmailStatusType.Sent,
                    EmailServerId = QueueEmail.Result.EmailServerId,
                    UniqueIdentity = QueueEmail.Result.UniqueIdentity
                });
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                await _QueueEmaillogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = QueueEmail.Result.Id,
                    Status = EmailStatusType.Canceled,
                    EmailServerId = QueueEmail.Result.EmailServerId,
                    UniqueIdentity = QueueEmail.Result.UniqueIdentity
                });
            }
        }
    }
}

