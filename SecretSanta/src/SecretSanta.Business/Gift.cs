using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class Gift
    {
        private string _Title = "invalid";
        private string _Description = "invalid";
        private string _Url = "invalid";
        private User _User = new User(1, "invalid", "invalid", new List<Gift>());

        public int Id { get; }

        public string Title
        {
            get => _Title;
            set => _Title = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Description
        {
            get => _Description;
            set => _Description = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Url
        {
            get => _Url;
            set => _Url = value ?? throw new ArgumentNullException(nameof(value));
        }

        public User User
        {
            get => _User;
            set => _User = value ?? throw new ArgumentNullException(nameof(value));
        }

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