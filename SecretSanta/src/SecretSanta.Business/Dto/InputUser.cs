using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class InputUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public int? SantaId { get; set; }
        [Required]
        public Data.User? Santa { get; set; }
        [Required]
        public IList<Data.Gift>? Gifts { get; } = new List<Data.Gift>();
        [Required]
        public IList<UserGroup>? UserGroups { get; } = new List<UserGroup>();
    }
}
