using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
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
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();

        public Group(string title)
        {
            Title = title;
        }
    }
}
