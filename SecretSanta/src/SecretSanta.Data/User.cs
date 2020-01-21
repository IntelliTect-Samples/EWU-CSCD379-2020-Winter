using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        public string FirstName { get => _FirstName; set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        private string _FirstName = string.Empty;
        public string LastName { get => _LastName; set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        private string _LastName = string.Empty;
        public User? Santa { get; set; } 

        public IList<Gift> Gifts { get => _Gifts; set => _Gifts = value ?? throw new ArgumentNullException(nameof(Gifts)); }
        private IList<Gift> _Gifts = null!;
        public IList<UserGroup> UserGroups { get => _UserGroups; set => _UserGroups = value ?? throw new ArgumentNullException(nameof(UserGroups)); }
        private IList<UserGroup> _UserGroups = null!;
    }
}
