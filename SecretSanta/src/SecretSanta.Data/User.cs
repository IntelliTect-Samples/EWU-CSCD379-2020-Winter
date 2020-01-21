﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
#nullable disable
        //private List<UserAndGroup> _UserAndGroup = new List<UserAndGroup>();
        public List<UserAndGroup> UserAndGroup { get; set; }
        public ICollection<Gift> Gifts { get; set; }
#nullable enable
        public string FirstName { get => _FirstName; set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        private string _FirstName = string.Empty;
        public string LastName { get => _LastName; set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        private string _LastName = string.Empty;
        
        public User? Santa {get ;set ;}

    }
}
