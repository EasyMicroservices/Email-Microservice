using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.Cores.Contracts.Requests;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailController : SimpleQueryServiceController<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long>
    {
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _contractlogic;
        private readonly IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> _sendEmaillogic;
        public EmailController(IContractLogic<SendEmailEntity, CreateSendEmailRequestContract, UpdateSendEmailRequestContract, SendEmailContract, long> sendEmaillogic, IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> contractlogic) : base(contractlogic)
        {
            _contractlogic = contractlogic;
            _sendEmaillogic = sendEmaillogic;
        }
        public override async Task<MessageContract<long>> Add(CreateEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkSendemailId = await _sendEmaillogic.GetById(new GetIdRequestContract<long>() { Id = request.SendEmailId });
            if (checkSendemailId.IsSuccess)
                return await base.Add(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "SendemailId is incorrect");
        }
        public override async Task<MessageContract<EmailContract>> Update(UpdateEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var checkSendemailId = await _sendEmaillogic.GetById(new GetIdRequestContract<long>() { Id = request.SendEmailId });
            if (checkSendemailId.IsSuccess)
                return await base.Update(request, cancellationToken);
            return (EasyMicroservices.ServiceContracts.FailedReasonType.Empty, "SendemailId is incorrect");

        }

    }
}
