using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public List<GroupSet> GroupSets
        {
            get => _GroupSets;
            set => _GroupSets = value ?? throw new ArgumentNullException(nameof(GroupSets));
        }

        private List<GroupSet> _GroupSets = new List<GroupSet>();
        private string _Name = string.Empty;

        public string Name
        {
            get => _Name;
            set => _Name = value ?? throw new ArgumentNullException(nameof(Name));
        }
    }
}