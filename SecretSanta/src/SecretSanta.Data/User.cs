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
        public IList<Gift> Gifts { get; } = new List<Gift>();
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}