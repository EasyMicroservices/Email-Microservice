using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using Microsoft.AspNetCore.Mvc;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : SimpleQueryServiceController<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long>
    {
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _contractlogic;
        private readonly IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> _QueueEmaillogic;

        public EmailController(IContractLogic<QueueEmailEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, QueueEmailContract, long> QueueEmaillogic, IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> contractlogic) : base(contractlogic)
        {
            _contractlogic = contractlogic;
            _QueueEmaillogic = QueueEmaillogic;
        }
        public override async Task<MessageContract<long>> Add(CreateEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueEmailId = await _QueueEmaillogic.GetById(new GetIdRequestContract<long>() { Id = request.QueueEmailId });;
            if (checkQueueEmailId.IsSuccess)
                return await base.Add(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "QueueEmailId is incorrect");
        }
        public override async Task<MessageContract<EmailContract>> Update(UpdateEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkQueueEmailId = await _QueueEmaillogic.GetById(new GetIdRequestContract<long>() { Id = request.QueueEmailId });
            if (checkQueueEmailId.IsSuccess)
                return await base.Update(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "QueueEmailId is incorrect");

        }

    }
}
