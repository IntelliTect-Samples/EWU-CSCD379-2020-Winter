using System;
using System.Collections.Generic;
using System.Text;
using static SecretSanta.Data.ProjectAsserts;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        public string FirstName {
            get => _FirstName;
            set => _FirstName = AssertStringNotNullOrEmpty(nameof(FirstName), value);
        }
        private string _FirstName = string.Empty;

        public string LastName
        {
            get => _LastName; 
            set => _LastName = AssertStringNotNullOrEmpty(nameof(LastName), value);
        }
        private string _LastName = string.Empty;

        public ICollection<Gift> Gifts { get; } = new List<Gift>();
        public List<UserGroup> UserGroups { get; } = new List<UserGroup>();
    }
}
