using System;
using System.Collections.ObjectModel;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Collection<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Gifts = new Collection<Gift>();
        }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}";
        }
    }
}