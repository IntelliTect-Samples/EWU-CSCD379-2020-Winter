using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : EntityBase
    {
#nullable disable
        public string Name { get => _Name; set => _Name = value ?? throw new ArgumentNullException(nameof(value)); }
        private string _Name = string.Empty;
        public IList<UserGroup> UserGroups { get; set; }

#nullable enable
    }
}
