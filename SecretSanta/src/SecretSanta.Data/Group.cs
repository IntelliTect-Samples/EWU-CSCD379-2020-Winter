using System;
using System.Collections.Generic;
using static SecretSanta.Data.ProjectAsserts;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
        public string Name
        {
            get=>_Name; 
            set=>_Name = AssertStringNotNullOrEmpty(nameof(Name), value);
        }
        private string _Name = String.Empty;
        public List<UserGroup> UserGroups { get=>_UserGroups; }
        private List<UserGroup> _UserGroups = new List<UserGroup>();
    }
}
