using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.Database.Schemas;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class SendEmailEntity : SendEmailSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public long QueueId { get; set; }
        public QueueEntity Queue { get; set; }
    }
}
