using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class CreateSendEmailRequestContract
    {
        public long EmailId { get; set; }
        public long QueueEmailId { get; set; }
        public string UniqueIdentity { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
