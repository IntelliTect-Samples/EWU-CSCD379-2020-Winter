using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
#nullable disable
        public User User { get; set; }
        public Group Group { get; set; }
#nullable enable
    }
}
