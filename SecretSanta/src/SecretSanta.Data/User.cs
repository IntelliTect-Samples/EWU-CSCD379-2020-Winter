using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        private string _FirstName = string.Empty;
        public string FirstName {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName));
        }

        private string _LastName = string.Empty;
        public string LastName {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName));
        }

        public User? Santa { get; set; }

#nullable disable
        public ICollection<Gift> Gifts { get; set; }

        public List<UserGroup> UserGroups { get; set; }
#nullable enable
    }
}
