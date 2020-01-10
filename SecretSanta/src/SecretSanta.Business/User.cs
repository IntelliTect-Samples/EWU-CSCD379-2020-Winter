using System.Collections.Generic;
namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Gift> Gifts { get; set; }

        public User(int id, string firstName,string lastName,List<Gift> gifts)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }
    }
}