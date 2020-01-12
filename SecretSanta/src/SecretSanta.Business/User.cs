using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }

        public string FirstName {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _FirstName = "";

        public string LastName
        {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _LastName = "";

        public List<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName, List<Gift>? gifts=null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts ?? new List<Gift>();
        }
    }
}