using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailController : SimpleQueryServiceController<EmailEntity, AddEmailContract, UpdateEmailContract, EmailContract, long>
    {
        public EmailController(IContractLogic<EmailEntity, AddEmailContract, UpdateEmailContract, EmailContract, long> contractReadable) : base(contractReadable)
        {

        }
    }
}
