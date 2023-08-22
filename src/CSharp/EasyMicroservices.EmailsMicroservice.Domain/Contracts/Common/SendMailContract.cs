using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Common
{
    public class SendMailContract
    {
        [EmailAddress]
        public string EmailAdress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
