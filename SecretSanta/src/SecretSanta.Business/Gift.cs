using System;

namespace SecretSanta.Business
{

    public class Gift
    {

        private readonly int _Id;

        public Gift(int id, string title, string description, string url, User user)
        {
            _Id          = id;
            Title       = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Url         = url ?? throw new ArgumentNullException(nameof(url));
            User        = user ?? throw new ArgumentNullException(nameof(user));
        }

        private string Title       { get; }
        private string Description { get; }
        private string Url         { get; }
        private User   User        { get; }

    }

}
