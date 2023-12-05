using EasyMicroservices.Cores.AspCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Contracts.Requests;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.EmailsMicroservice.Contracts.Common;
using EasyMicroservices.EmailsMicroservice.Contracts.Requests;
using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueueEmailController : SimpleQueryServiceController<QueueEntity, CreateQueueEmailRequestContract, UpdateQueueEmailRequestContract, EmailQueueContract, long>
    {
        public QueueEmailController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
