using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id {get;}

        private string _FirstName;
        public string FirstName {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _LastName;
        public string LastName {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public List<Gift> Gifts {get;} = new List<Gift>();

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }
    }
}
