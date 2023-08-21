using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.Database.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class SendEmailEntity : SendEmailSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public long EmailServerId { get; set; }
        public EmailServerEntity EmailServers { get; set; }
        //public long FromEmailId { get; set; }
        //public EmailEntity Emails { get; set; }
        public ICollection<EmailEntity> ToEmails { get; set; }

    }
}
