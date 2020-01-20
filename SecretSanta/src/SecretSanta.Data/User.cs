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
        public ICollection<Gift> Gifts { get=>_Gifts; }
        private ICollection<Gift> _Gifts = new List<Gift>();
        public List<UserGroup> UserGroups { get=>_UserGroups; }
        private List<UserGroup> _UserGroups = new List<UserGroup>();
    }
}
