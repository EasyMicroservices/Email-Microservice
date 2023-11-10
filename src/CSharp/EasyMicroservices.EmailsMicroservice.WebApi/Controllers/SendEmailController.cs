using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using EasyMicroservices.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class SendEmailController : SimpleQueryServiceController<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long>
    {
        private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _contractLogic;
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _emailLogic;
        private readonly IContractLogic<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long> _queueEmailLogic;
        private readonly IContractLogic<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailServerLogic;

        public IUnitOfWork unitOfWork;

        public SendEmailController(IUnitOfWork uow) : base(uow)
        {
            unitOfWork = uow;
            _contractLogic = uow.GetContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long>();
            _emailLogic = uow.GetContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long>();
            _emailServerLogic = uow.GetContractLogic<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long>();
            _queueEmailLogic = uow.GetContractLogic<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long>();
        }

        static HttpClient HttpClient = new HttpClient();
        public override async Task<MessageContract<long>> Add(CreateSendEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueId = await _queueEmailLogic.GetBy(x => true);
            if (!checkQueueId)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "QueueId is incorrect");
            var EmailServer = await _emailServerLogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.Result.ServerId });
            if (!EmailServer.IsSuccess)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId  is incorrect");
            var Email = await _emailLogic.GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.Result.FromEmailId });
            if (!Email.IsSuccess)
                return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "FromEmailId  is incorrect");

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
            var SenderEmail = Email.Result.Address;
            // Create the email message
            var message = new MailMessage
            {
                From = new MailAddress(SenderEmail), // Sender's email address
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.Body.Contains("<html", StringComparison.OrdinalIgnoreCase), // You can set this to false if you're sending plain text
            };

            if (!request.AttachmentFilesUrls.IsNullOrEmpty())
            {
                foreach (var attachUrl in request.AttachmentFilesUrls)
                {
                    var response = await HttpClient.GetAsync(attachUrl);
                    var fileName = response.Content.Headers.ContentDisposition?.FileName;
                    var fileBiteArr = await response.Content
                                            .ReadAsByteArrayAsync()
                                            .ConfigureAwait(false);
                    var memoryStream = new MemoryStream(fileBiteArr);
                    if (!fileName.HasValue())
                        fileName = Path.GetFileName(attachUrl);
                    message.Attachments.Add(new Attachment(memoryStream, fileName ?? "attactment"));
                }
            }

            if (!request.CC.IsNullOrEmpty())
            {
                foreach (var cc in request.CC)
                {
                    message.CC.Add(cc);
                }
            }

            message.To.Add(request.EmailAddress); // Recipient's email address

            try
            {
                await smtpClient.SendMailAsync(message);
                await _queueEmailLogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = checkQueueId.Result.Id,
                    Status = QueueStatusType.Sent,
                    ServerId = checkQueueId.Result.ServerId,
                    FromEmailId = checkQueueId.Result.FromEmailId,
                    UniqueIdentity = checkQueueId.Result.UniqueIdentity
                });
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                await _queueEmailLogic.Update(new UpdateQueueEmailRequestContract()
                {
                    Id = checkQueueId.Result.Id,
                    Status = QueueStatusType.Canceled,
                    ServerId = checkQueueId.Result.ServerId,
                    UniqueIdentity = checkQueueId.Result.UniqueIdentity,
                    FromEmailId = checkQueueId.Result.FromEmailId
                });
            }
            var addResult = await _queueEmailLogic.AddEntity(new QueueEntity()
            {
                FromEmailId = checkQueueId.Result.FromEmailId,
                ServerId = checkQueueId.Result.ServerId,
                Status = QueueStatusType.Sent,
            }, cancellationToken);
            return addResult.GetCheckedResult().Id;
        }
    }
}