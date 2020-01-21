using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public string Name { get => _Name; set => _Name = value ?? throw new ArgumentNullException(nameof(value)); }
        private string _Name = string.Empty;
        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
