using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class GroupInput
    {
        [Required]
        public string? Title { get; set; }
    }
}
