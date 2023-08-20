using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class CreateEmailRequestContract
    {
        public string Address { get; set; }
        public string UniqueIdentity { get; set; }
    }
}
