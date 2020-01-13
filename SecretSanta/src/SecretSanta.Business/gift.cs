using System;
using System.Collections.Generic;


namespace SecretSanta.Business
{
    class Gift
    {
        int Id {get;}
        string Title {get; set;}
        string Description {get; set;}
        string Url {get; set;}
        User User {get; set;}

        public Gift(int id, string title, string description, string url, User user)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            User = user ?? throw new ArgumentNullException(nameof(user));
        }
    }
}