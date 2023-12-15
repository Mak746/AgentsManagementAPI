using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AgentsDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lats Name is required")]
        public string LastName { get; set; }

        public byte[] LicenseAttachment { get; set; }
        public string LicenseAttachmentPath { get; set; }
        public byte[] AgentPhoto { get; set; }

        public string AgentPhotoPath { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed for Commission")]
        public int Commission { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Please select an image.")]

        public IFormFile FileImage { get; set; }

    }
}