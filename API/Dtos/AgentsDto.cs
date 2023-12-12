using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AgentsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public byte[] LicenseAttachment { get; set; }
        public string LicenseAttachmentPath { get; set; }
        public byte[] AgentPhoto { get; set; }

        public string AgentPhotoPath { get; set; }

        public DateTime DateOfBirth { get; set; }
        public int Commission { get; set; }
        public int CategoryId { get; set; }
    }
}