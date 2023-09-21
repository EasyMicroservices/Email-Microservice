using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class QueueEntity : QueueSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public long ServerId { get; set; }
        public long FromEmailId { get; set; }
        public EmailEntity FromEmail { get; set; }
        public ServerEntity Server { get; set; }
        public ICollection<SendEmailEntity> SendEmails { get; set; }
    }
}
