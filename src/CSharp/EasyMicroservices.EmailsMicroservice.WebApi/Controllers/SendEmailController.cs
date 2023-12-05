using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using EasyMicroservices.ServiceContracts;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class SendEmailController : SimpleQueryServiceController<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long>
    {
        readonly IUnitOfWork _unitOfWork;
        public SendEmailController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        static HttpClient HttpClient = new HttpClient();
        public override async Task<MessageContract<long>> Add(CreateSendEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueId = await _unitOfWork.GetLongLogic<QueueEntity>()
                .GetBy(x => true)
                .AsCheckedResult();

            var emailServer = await _unitOfWork.GetLongLogic<ServerEntity>()
                .GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.ServerId })
                .AsCheckedResult();

            var email = await _unitOfWork.GetLongLogic<EmailEntity>()
                .GetById(new Cores.Contracts.Requests.GetIdRequestContract<long> { Id = checkQueueId.FromEmailId })
                .AsCheckedResult();

            // Configure SMTP client settings
            var smtpClient = new SmtpClient
            {
                Host = emailServer.Address,          // Your SMTP server
                Port = emailServer.Port,                         // Port number
                EnableSsl = emailServer.IsSSL,                   // Use SSL
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailServer.Username, emailServer.Password) // Your SMTP credentials
            };
            var SenderEmail = email.Address;
            // Create the email message
            var message = new MailMessage
            {
                From = new MailAddress(SenderEmail), // Sender's email address
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.Body.Contains("<html", StringComparison.OrdinalIgnoreCase), // You can set this to false if you're sending plain text
            };

            if (request.AttachmentFilesUrls.HasAny())
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

            if (request.CC.HasAny())
            {
                foreach (var cc in request.CC.Where(x => x.HasValue()))
                {
                    message.CC.Add(cc);
                }
            }

            message.To.Add(request.EmailAddress); // Recipient's email address

            try
            {
                await smtpClient.SendMailAsync(message);
                await _unitOfWork.GetLongLogic<QueueEntity>().UpdateChangedValuesOnly(new QueueEntity()
                {
                    Id = checkQueueId.Id,
                    Status = QueueStatusType.Sent,
                    ServerId = checkQueueId.ServerId,
                    FromEmailId = checkQueueId.FromEmailId,
                    UniqueIdentity = checkQueueId.UniqueIdentity
                });
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                await _unitOfWork.GetLongLogic<QueueEntity>()
                    .UpdateChangedValuesOnly(new QueueEntity()
                    {
                        Id = checkQueueId.Id,
                        Status = QueueStatusType.Canceled,
                        ServerId = checkQueueId.ServerId,
                        UniqueIdentity = checkQueueId.UniqueIdentity,
                        FromEmailId = checkQueueId.FromEmailId
                    });
            }
            var addResult = await _unitOfWork.GetLongLogic<QueueEntity>()
                .AddEntity(new QueueEntity()
                {
                    FromEmailId = checkQueueId.FromEmailId,
                    ServerId = checkQueueId.ServerId,
                    Status = QueueStatusType.Sent,
                }, cancellationToken);
            return addResult.GetCheckedResult().Id;
        }
    }
}