using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class EmailServerController : SimpleQueryServiceController<ServerEntity, CreateEmailServerRequestContract, UpdateEmailServerRequestContract, EmailServerContract, long>
    {
        public IUnitOfWork unitOfWork;

        public EmailServerController(IUnitOfWork uow) : base(uow)
        {
            unitOfWork = uow;
        }
    }
}
