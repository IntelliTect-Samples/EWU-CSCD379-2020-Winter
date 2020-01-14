using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
 
        public List<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = new List<Gift>();
        }

        private string _FirstName = "<Invalid>";
        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = AssertIsNotNullOrWhitespace(value);
        }

        private string _LastName = "<Invalid>";
        public string LastName
        {
            get => _LastName;
            set => _LastName = AssertIsNotNullOrWhitespace(value);
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
