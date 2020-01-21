using System;
using System.Collections.ObjectModel;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public Collection<GroupSet> GroupSets
        {
            get => _GroupSets;
            set => _GroupSets = value ?? throw new ArgumentNullException(nameof(GroupSets));
        }

        private Collection<GroupSet> _GroupSets = new Collection<GroupSet>();
        private string _Name = string.Empty;
        public string Name
        {
            get => _Name;
            set => _Name = value ?? throw new ArgumentNullException(nameof(Name));
        }
    }
}