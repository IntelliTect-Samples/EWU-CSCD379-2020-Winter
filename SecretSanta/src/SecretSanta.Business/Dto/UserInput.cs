using SecretSanta.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Business.Dto
{
    public class UserInput
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public IList<Gift> Gifts { get; } = new List<Gift>();
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();

    }
}
