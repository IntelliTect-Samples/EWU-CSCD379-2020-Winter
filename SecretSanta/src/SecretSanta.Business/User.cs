using System.Collections.ObjectModel;

namespace SecretSanta.Business
{
    public class User
    {
        private readonly int _Id;
        private string _FirstName;
        private string _LastName;
        private Collection<Gift> _Gifts;

        public User(int id, string firstName, string lastName, Collection<Gift> gifts)
        {
            _Id = id;
            _FirstName = firstName;
            _LastName = lastName;
            _Gifts = gifts;
        }
    }
}