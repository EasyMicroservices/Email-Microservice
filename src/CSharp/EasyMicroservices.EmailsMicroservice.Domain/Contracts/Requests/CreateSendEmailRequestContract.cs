using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyMicroservices.EmailsMicroservice.Contracts.Requests
{
    public class CreateSendEmailRequestContract
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string UniqueIdentity { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> AttachmentFilesUrls { get; set; }
    }
}
