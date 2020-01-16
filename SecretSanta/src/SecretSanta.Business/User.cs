using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class User
    {
        private string _FirstName = "Invalid";
        private string _LastName = "Invalid";

        public int Id { get; }
        public string FirstName 
        { get => _FirstName; 
          set => _FirstName = value ?? throw new ArgumentNullException(nameof(value)); 
        }
        public string LastName 
        { 
            get => _LastName; 
            set => _LastName = value ?? throw new ArgumentNullException(nameof(value)); 
        }
        public List<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Gifts = new List<Gift>();
        }
    }
}
