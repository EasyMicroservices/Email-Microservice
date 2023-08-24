﻿using EasyMicroservices.EmailsMicroservice.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class CreateQueueEmailRequestContract
    {
        public long EmailServerId { get; set; }
        //public long  FromEmailId { get; set; }
        public EmailStatusType Status { get; set; }
        public string EmailAdress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UniqueIdentity { get; set; }

    }
}