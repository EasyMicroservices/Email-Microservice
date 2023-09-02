using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Castle.Components.DictionaryAdapter;
using EasyMicroservices.Cores.Database.Managers;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailController : SimpleQueryServiceController<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long>
    {
        private readonly IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> _contractlogic;

        public EmailController(IContractLogic<EmailEntity, CreateEmailRequestContract, UpdateEmailRequestContract, EmailContract, long> contractlogic) : base(contractlogic)
        {
            _contractlogic = contractlogic;
        }

        [HttpPost]
        public async Task<MessageContract<EmailContract>> FindByAddress(FindEmailByAddressRequestContract request) 
        {
            var decodedUniqueIdentity = DefaultUniqueIdentityManager.DecodeUniqueIdentity(request.UniqueIdentity);
            return await _contractlogic.GetBy(o => o.Address.Contains(request.EmailAddress) && DefaultUniqueIdentityManager.CutUniqueIdentity(o.UniqueIdentity, decodedUniqueIdentity.Count()) == request.UniqueIdentity);
        }
    }
}
