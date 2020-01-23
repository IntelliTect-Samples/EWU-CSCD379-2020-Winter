﻿using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class User : EntityBase
    {
        public string FirstName { get => _FirstName; set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        private string _FirstName = string.Empty;

        public string LastName { get => _LastName; set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        private string _LastName = string.Empty;

        public User? Santa { get; set; }

        public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
