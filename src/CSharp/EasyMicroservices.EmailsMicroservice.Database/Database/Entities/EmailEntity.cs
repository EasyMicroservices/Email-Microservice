using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class EmailEntity : EmailSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public ICollection<QueueEmailEntity> QueueEmails { get; set; }
        public ICollection<SendEmailEntity> SendEmails { get; set; }
     }
}