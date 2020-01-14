using System;
using System.Collections.ObjectModel;

namespace SecretSanta.Business
{

    public class User
    {

        private readonly int _Id;

        public User(int id, string firstName, string lastName)
        {
            _Id       = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName  = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Gifts     = new Collection<Gift>();
        }

        private string           FirstName { get; }
        private string           LastName  { get; }
        private Collection<Gift> Gifts     { get; }

    }

}
