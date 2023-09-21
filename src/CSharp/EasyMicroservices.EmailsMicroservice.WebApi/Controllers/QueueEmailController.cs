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
    public class QueueEmailController : SimpleQueryServiceController<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long>
    {
        private readonly IContractLogic<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        private readonly IContractLogic<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long> _contralogic;
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _emailcontract;



        public QueueEmailController(IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> emailcontract, IContractLogic<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic, IContractLogic<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long> contractLogic) : base(contractLogic)
        {
            _emailserverlogic = emailserverlogic;
            _contralogic = contractLogic;
            _emailcontract = emailcontract;
        }
        public override async Task<MessageContract<long>> Add(CreateQueueEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var emailserverId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.EmailServerId });
            var checkEmailId = await _emailcontract.GetById(new GetIdRequestContract<long>() { Id = request.FromEmailId });
            if (emailserverId.IsSuccess && checkEmailId.IsSuccess)
                return await base.Add(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId or FromEmailId is incorrect");
        }
        public override async Task<MessageContract<EmailQueueContract>> Update(UpdateQueueEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var emailserverId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.ServerId });
            var checkEmailId = await _emailcontract.GetById(new GetIdRequestContract<long>() { Id = request.FromEmailId });
            if (emailserverId.IsSuccess && checkEmailId.IsSuccess)
                return await base.Update(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId is incorrect");
        }
    }
}
