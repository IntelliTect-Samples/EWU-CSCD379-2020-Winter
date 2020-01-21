using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        private string _FirstName = null!;
        public string FirstName
        {
            get { return _FirstName; }
            set 
            {
                AssertIsNotNullOrWhitespace(value);
                _FirstName = value;
            }
        }

        private string _LastName = null!;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _LastName = value;
            }
        }
        public int? SantaId { get; set; }
        public User? Santa { get; set; }
#nullable disable
        public IList<Gift> Gifts { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
#nullable enable

    }
}