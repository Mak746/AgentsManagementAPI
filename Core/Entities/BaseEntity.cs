using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

    }
}