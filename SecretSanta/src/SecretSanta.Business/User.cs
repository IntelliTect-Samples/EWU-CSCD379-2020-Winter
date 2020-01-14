using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Gift> Gifts { get; private set; }

        public User(int id, string firstName, string lastName, List<Gift> gifts)
        {
            Id = id;
            FirstName = firstName ?? throw new System.ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new System.ArgumentNullException(nameof(lastName));
            Gifts = gifts ?? throw new System.ArgumentNullException(nameof(gifts));
        }
    }
}