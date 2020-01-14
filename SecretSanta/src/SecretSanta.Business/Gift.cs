namespace SecretSanta.Business
{
    public class Gift
    {
        private readonly int _Id;
        private string _Title;
        private string _Description;
        private string _Url;
        private string _User;

        public Gift(int id, string title, string description, string url, string user)
        {
            _Id = id;
            _Title = title;
            _Description = description;
            _Url = url;
            _User = user;
        }
    }
}