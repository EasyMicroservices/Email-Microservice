using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class UpdateEmailRequestContract
    {
        public long Id { get; set; }
        [EmailAddress]
        public string Address { get; set; }
        public long QueueEmailId { get; set; }

        public string UniqueIdentity { get; set; }
    }
}
