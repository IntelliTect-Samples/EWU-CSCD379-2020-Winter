namespace SecretSanta.Data
{
    public class Gift : FingerPrintEntityBase
    {
        public string Title
        {
            get => _Title;
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _Title = value;
            }
        }
        private string _Title = string.Empty;

        public string Description
        {
            get => _Description;
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _Description = value;
            }
        }
        private string _Description = string.Empty;

        public string Url
        {
            get => _Url;
            set
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

        public Gift(string title, string url, string description, User user) : this(title, url, description)
        {
            User = user;
        }

        public Gift(string title, string url, string description) : this(title, url)
        {
            Description = description;
        }

        // Constructor for entity framework
        private Gift(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}
