using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }

        private string _Title = "<Invalid>";
        public string Title
        {
            get => _Title;
            set => _Title = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _Description = "<Invalid>";
        public string Description
        {
            get => _Description;
            set => _Description = value ?? throw new ArgumentNullException(nameof(value));
        }

        private string _Url = "<Invalid>";
        public string Url
        {
            get => _Url;
            set => _Url = value ?? throw new ArgumentNullException(nameof(value));
        }

        public User? User { get; set; }

        public Gift(int id, string title, string description, string url, User? user)
        {
            Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }
    }
}
