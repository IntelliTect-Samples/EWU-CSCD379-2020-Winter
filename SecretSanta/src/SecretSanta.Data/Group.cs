using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public string Name { get=>_Name; set=>_Name = value ?? throw new ArgumentNullException(nameof(Name)); }
        private string _Name = String.Empty;
        public List<UserGroup> UserGroups { get=>_UserGroups; }
        private List<UserGroup> _UserGroups = new List<UserGroup>();
    }
}
