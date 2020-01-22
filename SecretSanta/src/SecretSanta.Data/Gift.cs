﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

       
        [DisallowNull]
        private User? _User;
        public User User { get=>_User!; set=>_User=value?? throw new ArgumentNullException(nameof(User)); }


       
        private Gift(string title, string description, string url)
        {
            Title = title;
            Description = description;
            Url = url;
        }

        //if every gift requires a user then this:
        public Gift(string title, string description, string url, User user) : this(title, description, url)
        {

            User = user;
        }

    }
}
