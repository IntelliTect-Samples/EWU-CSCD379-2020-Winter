using System;
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
#nullable disable
        public User User { get; set; }
#nullable enable
        public int UserId { get; set; }
        public User(string title, string description, User user) :
            this(title, description)
        {
            User = user;
        }

// Justification: Used by entity framework to instantiate object from the database.
#pragma warning disable IDE0051 // Remove unused private members.  Ignore because the constructor is used by entity framework.
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        private User(
#nullable enable
#pragma warning restore IDE0051 // Remove unused private members
            string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
