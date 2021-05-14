using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleModel.Entities
{
    public class Email : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string EmailAddress { get; set; }
    }
}
