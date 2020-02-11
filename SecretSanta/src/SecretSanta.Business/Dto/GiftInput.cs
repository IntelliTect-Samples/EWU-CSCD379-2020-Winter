using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class GiftInput
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        [Required]
        public int? UserId { get; set; }

      
    }
}
