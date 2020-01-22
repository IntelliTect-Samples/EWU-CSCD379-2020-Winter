﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        
        public string FirstName { get => _FirstName; set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        private string _FirstName = string.Empty;
        public string LastName { get => _LastName; set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        private string _LastName = string.Empty;

      

        public ICollection<Gift> Gifts { get; } = new List<Gift>();


        [AllowNull]
        public User? Santa { get; set; }

        public List<UserGroup> UserGroups { get; } = new List<UserGroup>();

        private User() { }
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            
        }
    }
}
