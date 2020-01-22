using System.Collections.Generic;

namespace SecretSanta.Data
{

    public class Group : FingerPrintEntityBase
    {

        public string Name { get; set; }

        public ICollection<UserGroupRelation> Relations { get; } = new List<UserGroupRelation>();

    }

}
