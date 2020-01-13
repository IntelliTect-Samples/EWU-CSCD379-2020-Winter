
using System.Collections.Generic;


namespace SecretSanta.Business
{
    class Gift
    {
        int Id {get;}
        string Title;
        string Description;
        string Url;
        User User;

        public Gift(int id, string title, string description, string url, User user)
        {
            Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }
    }
}