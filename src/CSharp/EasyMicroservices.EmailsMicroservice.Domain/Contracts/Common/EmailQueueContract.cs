using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Common
{
    public class EmailQueueContract : IUniqueIdentitySchema, ISoftDeleteSchema, IDateTimeSchema
    {
        public long Id { get; set; }
        public long ServerId { get; set; }
        public long FromEmailId { get; set; }
        public QueueStatusType Status { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
