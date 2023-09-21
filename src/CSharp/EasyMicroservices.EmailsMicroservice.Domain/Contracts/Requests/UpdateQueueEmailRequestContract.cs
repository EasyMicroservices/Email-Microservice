using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class UpdateQueueEmailRequestContract
    {
        public long Id { get; set; }
        public long ServerId { get; set; }
        public long FromEmailId { get; set; }
        public QueueStatusType Status { get; set; }
        public string UniqueIdentity { get; set; }

    }
}
