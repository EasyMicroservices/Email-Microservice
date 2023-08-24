using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class SendEmailController : SimpleQueryServiceController<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long>
    {
        private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _contractlogic;
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _emaillogic;
        private readonly IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> _QueueEmaillogic;
        private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        public SendEmailController(IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> QueueEmaillogic, IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> emaillogic, IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic ,  IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> contractlogic) : base(contractlogic)
        {
            _contractlogic = contractlogic;
            _emaillogic = emaillogic;
            _emailserverlogic = emailserverlogic;
            _QueueEmaillogic = QueueEmaillogic;
        }
        public override async Task<MessageContract<long>> Add(CreateSendEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueId = await _QueueEmaillogic.GetById( new Cores.Contracts.Requests.GetIdRequestContract<long>() { Id = request.QueueEmailId});
            if (!checkQueueId.IsSuccess)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "QueueId is incorrect");
            var EmailServer = await _emailserverlogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.Result.EmailServerId });
            if (!EmailServer.IsSuccess)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId  is incorrect");
            var Email = await _emaillogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.Result.FromEmailId });
            if (!Email.IsSuccess)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "FromEmailId  is incorrect");

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
            var SenderEmail = Email.Result.Address;
            // Create the email message
            var message = new MailMessage
            {
                From = new MailAddress(SenderEmail), // Sender's email address
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = false, // You can set this to false if you're sending plain text
            };
            message.To.Add(request.EmailAddress); // Recipient's email address

            try
            {
                await smtpClient.SendMailAsync(message);
                await _QueueEmaillogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = checkQueueId.Result.Id,
                    Status = EmailStatusType.Sent,
                    EmailServerId = checkQueueId.Result.EmailServerId,
                    UniqueIdentity = checkQueueId.Result.UniqueIdentity
                });
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                await _QueueEmaillogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = checkQueueId.Result.Id,
                    Status = EmailStatusType.Canceled,
                    EmailServerId = checkQueueId.Result.EmailServerId,
                    UniqueIdentity = checkQueueId.Result.UniqueIdentity
                });
            }
            return await base.Add(request, cancellationToken);
        }
    }
}
