using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    class User
    {
        int Id {get;}
        string FirstName {get; set;}
        string LastName {get; set;}

        IList<Gift> GiftList {get;}

        public User(int id, string firstName, string lastName, List<Gift> giftList)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            GiftList = giftList ?? new List<Gift>();
        }
    }
}