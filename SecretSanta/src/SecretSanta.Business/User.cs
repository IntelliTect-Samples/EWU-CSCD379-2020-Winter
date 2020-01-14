using System.Collections.Generic;
using System;

namespace SecretSanta.Business
{
    public class User
    {
        private readonly int _Id = 0;

        public int Id
        {
            get => _Id;
        }

        private string _FirstName = "<firstName>";

        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = AssertIsNotNullOrWhitespace(value);
        }

        private string _LastName = "<lastName>";

        public string LastName
        {
            get => _LastName;
            set => _LastName = AssertIsNotNullOrWhitespace(value);
        }

        private List<Gift> _Gifts = new List<Gift>();

        public List<Gift> Gifts
        {
            get => _Gifts;
            set => _Gifts = value ?? throw new ArgumentNullException(nameof(value));
        }

        public User(int id, string firstName, string lastName, List<Gift> gifts)
        {
            _Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }


        private string AssertIsNotNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrWhiteSpace(temp) =>
                    throw new ArgumentException($"{nameof(value)} cannot be only whitespace.", nameof(value)),
                _ => value
            };

    }
}