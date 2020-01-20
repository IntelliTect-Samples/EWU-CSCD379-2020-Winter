using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public string Name { get; set; }
        public List<UserGroup> UserGroups { get; set; }
    }
}
