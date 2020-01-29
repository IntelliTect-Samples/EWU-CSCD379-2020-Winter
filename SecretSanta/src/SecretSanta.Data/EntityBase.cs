using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        [Required]
        public int Id { get; protected set; }
    }
}
