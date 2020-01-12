using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    class Gift
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public User User { get; set; }

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
