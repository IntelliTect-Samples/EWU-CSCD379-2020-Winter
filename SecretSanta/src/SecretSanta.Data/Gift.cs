using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Gift : FingerPrintEntityBase
    {
        public string Title { get => _Title; set => _Title = value ?? throw new ArgumentNullException(nameof(Title)); }
        private string _Title = string.Empty;
        public string Description { get => _Description; set => _Description = value ?? throw new ArgumentNullException(nameof(Description)); }
        private string _Description = string.Empty;
        public string Url { get => _Url; set => _Url = value ?? throw new ArgumentNullException(nameof(Url)); }
        private string _Url = string.Empty;
        public User User { get; set; }

        public Gift(int id, string title, string description, string url, User user)
            : base(id, user.FullName, DateTime.Now, user.FullName, DateTime.Now)
        {
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }

        public Gift(int id, string createdBy, DateTime createdOn, string modifiedBy, DateTime modifiedOn,
            string title, string description, string url, User user)
            : base(id, createdBy, createdOn, modifiedBy, modifiedOn)
        {
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }
    }
}
