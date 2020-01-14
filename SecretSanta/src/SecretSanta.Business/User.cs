using System.Collections.ObjectModel;

namespace SecretSanta.Business
{
    public class User
    {
        private readonly int _Id;
        private string _FirstName;
        private string _LastName;
        public Collection<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName)
        {
            _Id = id;
            _FirstName = firstName;
            _LastName = lastName;
            Gifts = new Collection<Gift>();
        }

        public override string ToString()
        {
            return $"{nameof(_FirstName)}: {_FirstName}, {nameof(_LastName)}: {_LastName}";
        }
    }
}