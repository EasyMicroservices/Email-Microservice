using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Cores.Database.Managers;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailController : SimpleQueryServiceController<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long>
    {
        readonly IUnitOfWork _unitOfWork;
        public EmailController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public override async Task<MessageContract<long>> Add(CreateEmailRequestContract request, CancellationToken cancellationToken = default)
        {
            var find = await _unitOfWork.GetLongLogic<EmailEntity>().GetBy(x => x.Address == request.Address);
            if (!find)
                return await base.Add(request, cancellationToken);
            return find.Result.Id;
        }

        [HttpPost]
        public async Task<MessageContract<EmailContract>> FindByAddress(FindEmailByAddressRequestContract request)
        {
            var decodedUniqueIdentity = DefaultUniqueIdentityManager.DecodeUniqueIdentity(request.UniqueIdentity);

            var emails = await _unitOfWork.GetLongContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract>()
                .GetAll(query => query.Where(o => o.Address.Contains(request.EmailAddress)));

            var filteredEmail = emails.Result
                .Where(o => DefaultUniqueIdentityManager.CutUniqueIdentity(o.UniqueIdentity, decodedUniqueIdentity.Count()) == request.UniqueIdentity)
                .FirstOrDefault();

            return filteredEmail;
        }
    }
}
