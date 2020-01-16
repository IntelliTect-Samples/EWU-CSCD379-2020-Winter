using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
        private string _Title = "Invalid";
        private string _Description = "Invalid";
        private string _Url = "Invalid";

        public int Id { get; } 
        public string Title 
        { get => _Title; 
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
        public User User { get; set; }

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
