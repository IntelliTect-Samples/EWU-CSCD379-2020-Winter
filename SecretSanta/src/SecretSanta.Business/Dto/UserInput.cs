﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class UserInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? SantaId { get; set; }
        public User? Santa { get; set; }
        public IList<Gift> Gifts { get; } = new List<Gift>();
        
    }
}
