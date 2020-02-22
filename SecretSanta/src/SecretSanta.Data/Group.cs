using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        private string? _Title;

        public string Title { get => _Title!; set => _Title = value ?? throw new ArgumentNullException(nameof(value)); }
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();

        public Group(string title)
        {
            Title = title;
        }
    }
}
