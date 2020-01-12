using System.Collections.Generic;
using System;
namespace SecretSanta.Business
{
    public class User
    {
        private string _FirstName;
        private string _LastName;
        private List<Gift> _Gifts;

        public int Id { get; }
        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string LastName
        {
            get => _LastName;
            set => _LastName = value ?? throw new ArgumentNullException(nameof(value));
        }
        public List<Gift> Gifts 
        {
            get => _Gifts; 
            set => _Gifts = value ?? throw new ArgumentNullException(nameof(value));
        }

        public User(int id, string firstName, string lastName, List<Gift> gifts)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }
    }
}