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
        public ICollection<Gift> Gifts { get; set; }
    }
}