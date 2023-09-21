using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.Database.Schemas;
using System.Collections.Generic;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class ServerEntity : ServerSchema, IIdSchema<long>
    {
        public long Id { get; set; }
        public ICollection<QueueEntity> Queues { get; set; }
    }
}
