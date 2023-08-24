using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.Database.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class SendEmailEntity : SendEmailSchema , IIdSchema<long>
    {
        public long Id { get; set; }
        public long QueueEmailId { get; set; }
        public QueueEmailEntity QueueEmails { get; set; }

    }
}
