using System;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class Gift
    {
        private readonly int _Id = 0;

        public int Id
        {
            get => _Id;
        }

        private string _Title = "<Title>";

        public string Title
        {
            get => _Title;
            set => _Title = AssertIsNotNullOrWhitespace(value);
        }

        private string _Description = "<Descrption>";

        public string Description
        {
            get => _Description;
            set => _Description = AssertIsNotNullOrWhitespace(value);
        }

        private string _Url = "<Url>";

        public string Url
        {
            get => _Url;
            set => _Url = AssertIsNotNullOrWhitespace(value);
        }

        private User _User = new User(0, "<fistName>", "<lastName>", new List<Gift>());

        public User User
        {
            get => _User;
            set => _User = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Gift(int id, string title, string description, string url, User user)
        {
            _Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }

        private string AssertIsNotNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrWhiteSpace(temp) =>
                    throw new ArgumentException($"{nameof(value)} cannot be only whitespace.", nameof(value)),
                _ => value
            };

    }
}
