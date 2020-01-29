using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Gift : FingerPrintEntityBase
    {
        public string Title
        {
            get => _Title; set
            {
                AssertIsNotNullOrWhitespace(value);
                _Title = value;
            }
        }
        private string _Title = string.Empty;

        public string Description
        {
            get => _Description; set
            {
                AssertIsNotNullOrWhitespace(value);
                _Description = value;
            }
        }
        private string _Description = string.Empty;

        public string Url
        {
            get => _Url; set
            {
                AssertIsNotNullOrWhitespace(value);
                _Url = value;
            }
        }
        private string _Url = string.Empty;

#nullable disable
        public User User { get; set; }
#nullable enable
        public int UserId { get; set; }

        

        public Gift(string title, string description, string url, User user) : this(title, description, url, user.Id)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        private Gift(string title, string description, string url, int userId)
        {
            Title = title;
            Url = url;
            Description = description;
            UserId = userId;
        }
    }
}
