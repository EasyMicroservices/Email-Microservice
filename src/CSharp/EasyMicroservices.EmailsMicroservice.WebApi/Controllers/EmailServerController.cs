using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailServerController : SimpleQueryServiceController<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long>
    {
        public EmailServerController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
