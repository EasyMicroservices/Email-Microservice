using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Cores.Interfaces;

namespace EasyMicroservices.EmailsMicroservice.WebApi.Controllers
{
    public class FindEmailByAddressRequestContract : IUniqueIdentitySchema
    {
        public string EmailAddress { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
