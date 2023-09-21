using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;

namespace EasyMicroservices.EmailsMicroservice.Database.Schemas
{
    public class QueueSchema : IUniqueIdentitySchema, IDateTimeSchema, ISoftDeleteSchema
    {
        public QueueStatusType Status { get; set; }
        public string UniqueIdentity { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
