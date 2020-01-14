using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id {get;}
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public IList<Gift> GiftList {get;}

        public User(int id, string firstName, string lastName, List<Gift> giftList)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            GiftList = giftList ?? new List<Gift>();
        }
    }
}