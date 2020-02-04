using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public string Title { get; set; }
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();

        public Group(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}
