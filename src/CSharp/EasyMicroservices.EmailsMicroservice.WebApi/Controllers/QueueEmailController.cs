using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.Cores.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using System.Net.Mail;
using System.Net;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueueEmailController : SimpleQueryServiceController<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long>
    {
        private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        private readonly IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> _contralogic;

        public QueueEmailController(IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic, IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> contractLogic) : base(contractLogic)
        {
            _emailserverlogic = emailserverlogic;
            _contralogic = contractLogic;
        }
        public override async Task<MessageContract<long>> Add(CreateQueueEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueEmailId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.EmailServerId });
            if (checkQueueEmailId.IsSuccess)
                return await base.Add(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId is incorrect");
        }
        public override async Task<MessageContract<QueueEmailContract>> Update(UpdateQueueEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueEmailId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.EmailServerId });
            if (checkQueueEmailId.IsSuccess)
                return await base.Update(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId is incorrect");
        }
    }
}
