using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = AssertIsNullOrWhitespace(value);
        }
        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set => _LastName = AssertIsNullOrWhitespace(value);
        }
        private List<Gift> _Gift;
        public List<Gift> Gifts
        {
            get => _Gift;
            set => _Gift = value ?? throw new ArgumentNullException(nameof(value));
        }


#nullable disable // Properties are initialized and checked in set method.
        public User(int id, string firstName, string lastName, List<Gift> gifts)
#nullable enable
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }

        private string AssertIsNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrWhiteSpace(temp) => throw new ArgumentException($"{nameof(value)} cannot be just whitespace.", nameof(value)),
                _ => value
            };
    }
}