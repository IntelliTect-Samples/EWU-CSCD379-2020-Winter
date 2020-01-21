using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName));
        }

        private string _FirstName = string.Empty;

        public string LastName
        {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName));
        }

        private string _LastName = string.Empty;

        public ICollection<Gift> Gifts
        {
            get => _Gifts;
            set => _Gifts = value ?? throw new ArgumentNullException(nameof(Gifts));
        }

        private ICollection<Gift> _Gifts = new List<Gift>();
        public User? Santa { get; set; }

        public List<GroupSet> GroupSets
        {
            get => _GroupSets;
            set => _GroupSets = value ?? throw new ArgumentNullException(nameof(GroupSets));
        }

        private List<GroupSet> _GroupSets = new List<GroupSet>();
    }
}
