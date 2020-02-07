using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class InputGift
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Url { get; set; }
        [Required]
        public int? UserId { get; set; }
    }
}
