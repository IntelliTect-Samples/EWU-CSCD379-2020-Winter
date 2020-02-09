using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class GiftInput : IEntity
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }

        public int Id { get; set; }
    }
}
