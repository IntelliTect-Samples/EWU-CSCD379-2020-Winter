using System;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }

#pragma warning disable CA1056 // Uri properties should not be strings, or so system says but listening to directions!
        public string Url { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings, or so system says but listening to directions!

        public string Description { get; set; }

        public string Title
        {
            get=>_Title;
            set=>_Title = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _Title = "";

        public User User { get; set; }

#pragma warning disable CA1054 // Uri parameters should not be strings, but listening to directions!
        public Gift(int id, string title, string description, string url, User user)
#pragma warning restore CA1054 // Uri parameters should not be strings, but listening to directions!
        {
            Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }
    }
}