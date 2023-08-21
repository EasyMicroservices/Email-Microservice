using EasyMicroservices.EmailMicroservice.Database.Schemas;
using EasyMicroservices.Cores.Database.Interfaces;
using EasyMicroservices.Cores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.ComponentModel.DataAnnotations;

namespace EasyMicroservices.EmailsMicroservice.Database.Entities
{
    public class EmailEntity : EmailSchema, IIdSchema<long>
    {
        public long SendEmailId { get; set; }
        public SendEmailEntity SendEmails { get; set; }
        public long Id { get; set; }
    }
}