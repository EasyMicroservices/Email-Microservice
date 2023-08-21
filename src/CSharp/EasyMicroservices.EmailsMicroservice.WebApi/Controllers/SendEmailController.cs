using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.Cores.Contracts.Requests;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class SendEmailController : SimpleQueryServiceController<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long>
    {
        private readonly IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> _emailserverlogic;
        private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _contralogic;

        public SendEmailController(IContractLogic<EmailServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long> emailserverlogic, IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> contractLogic) : base(contractLogic)
        {
            _emailserverlogic = emailserverlogic;
            _contralogic = contractLogic;
        }
        public override async Task<MessageContract<long>> Add(CreateSendEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkSendemailId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.EmailServerId });
            if (checkSendemailId.IsSuccess)
                return await base.Add(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId is incorrect");
        }
        public override async Task<MessageContract<SendEmailContract>> Update(UpdateSendEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkSendemailId = await _emailserverlogic.GetById(new GetIdRequestContract<long>() { Id = request.EmailServerId });
            if (checkSendemailId.IsSuccess)
                return await base.Update(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "EmailServerId is incorrect");

        }
    }
}
