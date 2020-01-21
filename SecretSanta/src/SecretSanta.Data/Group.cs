using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
#nullable disable
        public string Name { get; set; }

        public List<UserGroup> UserGroups { get; set; }
#nullable enable
    }
}
