using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : EntityBase
    {
        public string Name { get; set; }
        public IList<UserGroup> UserGroups { get; set; }

        public Group(string name)
        {
            Name = name;
        }
    }
}
