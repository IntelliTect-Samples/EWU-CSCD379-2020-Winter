using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }

        private string _FirstName = "<Invalid>";
        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _LastName = "<Invalid>";
        public string LastName
        {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public List<Gift>? Gifts { get; set; }

        public User(int id, string firstName, string lastName, List<Gift>? gifts)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }
    }
}
