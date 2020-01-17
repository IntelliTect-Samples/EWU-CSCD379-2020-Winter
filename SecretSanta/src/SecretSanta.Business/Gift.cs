using System;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }
        private string _Title;
        public string Title
        {
            get => _Title;
            set => _Title = AssertIsNullOrWhitespace(value);
        }
        private string _Description;
        public string Description
        {
            get => _Description;
            set => _Description = AssertIsNullOrWhitespace(value);
        }
        private string _Url;
        public string Url
        {
            get => _Url;
            set => _Url = AssertIsNullOrWhitespace(value);
        }
        private User _User;
        public User User
        {
            get => _User;
            set => _User = value ?? throw new ArgumentNullException(nameof(value));
        }

#nullable disable // Properties are initialized and checked in set method.
        public Gift(int id, string title, string description, string url, User user)
#nullable enable
        {
            Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }

        private string AssertIsNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrWhiteSpace(temp) => throw new ArgumentException($"{nameof(value)} cannot be just whitespace.", nameof(value)),
                _ => value
            };
    }
}
