using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class InputGroup
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public IList<UserGroup>? UserGroups { get; } = new List<UserGroup>();
    }
}
