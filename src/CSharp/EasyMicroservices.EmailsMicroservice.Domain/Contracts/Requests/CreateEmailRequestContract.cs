using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class CreateEmailRequestContract
    {
        [EmailAddress]
        public string Address { get; set; }
        public long SendEmailId { get; set; }

        public string UniqueIdentity { get; set; }
    }
}
