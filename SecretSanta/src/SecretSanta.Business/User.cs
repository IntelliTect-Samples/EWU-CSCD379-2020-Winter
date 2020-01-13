using System.Collections.Generic;

namespace SecretSanta.Business
{
    class User
    {
        int Id {get;}
        string FirstName;
        string LastName;

        IList<Gift> GiftList = new List<Gift>();

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}