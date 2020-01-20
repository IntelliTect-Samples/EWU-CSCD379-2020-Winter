﻿using System;
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
        public ICollection<Gift> Gifts { get; set; }
        public List<UserGroup> UserGroups { get; set; }
       // public Nullable<User> Santa { get; set; } nullable property? something may need to be changed to make this work. or its something else
       public User? Santa { get; set; } //this may make a it a nullable property... unsure.

       /* public User(int id, string firstName, string lastName, List<Gift> gifts)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }
        */
    }
}
