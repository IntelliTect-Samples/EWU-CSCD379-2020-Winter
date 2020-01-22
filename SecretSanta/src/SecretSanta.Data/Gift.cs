using System;
using static SecretSanta.Data.ProjectAsserts;

namespace SecretSanta.Data
{
    public class Gift : FingerPrintEntityBase
    {
        public string Title { 
            get => _Title;
            set => _Title = AssertStringNotNullOrEmpty(nameof(Title), value);
        }
        private string _Title = string.Empty;

        public string Description
        {
            get => _Description; 
            set => _Description = AssertStringNotNullOrEmpty(nameof(Description), value);
        }
        private string _Description = string.Empty;

        public string Url
        {
            get => _Url; 
            set => _Url = AssertStringNotNullOrEmpty(nameof(Url), value);
        }
        private string _Url = string.Empty;

        public User User { get=>_User; set=>_User=value??throw new ArgumentNullException(nameof(User)); }
        private User _User = new User();
    }
}
