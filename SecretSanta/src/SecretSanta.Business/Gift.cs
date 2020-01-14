namespace SecretSanta.Business
{
    public class Gift
    {
        private readonly int _Id;
        public string Title { get; }
        public string Description { get; }
        public string Url { get; }
        public User User { get; }

        public Gift(int id, string title, string description, string url, User user)
        {
            _Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
            User?.Gifts.Add(this);
        }

    }
}