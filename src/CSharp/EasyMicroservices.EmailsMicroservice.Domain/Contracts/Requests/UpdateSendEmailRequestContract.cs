using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class UpdateSendEmailRequestContract
    {
        public long Id { get; set; }
        public long EmailServerId { get; set; }
        //public string FromEmailId { get; set; }
        public EmailStatusType Status { get; set; }
    }
}
