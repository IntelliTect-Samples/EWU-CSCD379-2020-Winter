using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }
        
        public User? User { get; set; }
        private string _Title = "<Invalid>";
        public string Title
        {
            get => _Title;
            set => _Title = AssertIsNotNullOrWhitespace(value);
        }

        private string _Description = "<Invalid>";
        public string Description
        {
            get => _Description;
            set => _Description = AssertIsNotNullOrWhitespace(value);
        }

        private string _Url = "<Invalid>";
        public string Url
        {
            get => _Url;
            set => _Url = AssertIsNotNullOrWhitespace(value);
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
