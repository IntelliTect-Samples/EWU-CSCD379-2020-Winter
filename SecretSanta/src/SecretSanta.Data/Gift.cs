﻿using System;

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
#nullable disable
        public User User { get; set; }
#nullable enable
        public int UserId { get; set; }

        public Gift(string title, string description, string url, User user) : this(title, description, url,
#pragma warning disable CA1062 // Cannot be null
            user.Id)
        #pragma warning restore CA1062
        {
            User = user;
        }
#nullable disable
        private Gift(string title, string description, string url, int userId)
    #nullable enable
        {
            Title = title;
            Description = description;
            Url = url;
            UserId = userId;
        }
    }
}
