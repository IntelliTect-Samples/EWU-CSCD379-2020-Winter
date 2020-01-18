using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class UserGroup
    {
#nullable disable
        public User User { get; set; }
        public Group Group { get; set; }
#nullable enable
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
