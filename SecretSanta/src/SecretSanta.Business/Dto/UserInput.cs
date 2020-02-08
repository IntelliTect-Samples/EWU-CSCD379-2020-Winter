﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class UserInput
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public IList<Gift>? Gifts { get; }
    }
}
