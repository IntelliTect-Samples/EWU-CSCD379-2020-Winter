namespace BlogEngine.Data
{
    public class Comment : FingerPrintEntityBase
    {
        public Comment(string name, string email, string content, int postId)
        {
            Name = name;
            Email = email;
            Content = content;
            PostId = postId;
        }
        private string _Name = null!;
        public string Name
        {
            get { return _Name; }
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _Name = value;
            }
        }
        private string _Email = null!;
        public string Email
        {
            get { return _Email; }
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _Email = value;
            }
        }
        private string _Content = null!;
        public string Content
        {
            get { return _Content; }
            set
            {
                AssertIsNotNullOrWhitespace(value);
                _Content = value;
            }
        }
        public int PostId { get; set; }

    }
}