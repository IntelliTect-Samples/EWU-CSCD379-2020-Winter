using System;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Title
        {
            get=>_Title;
            set=>_Title = value ?? throw new ArgumentNullException(nameof(value));
        }
        private string _Title = "";

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