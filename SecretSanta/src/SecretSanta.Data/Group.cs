using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class Group : FingerPrintEntityBase
    {
#nullable disable // Properties are initialize and checked in the set method.
        private string _Name;
        public string Name
        {
            get => _Name;
            set => _Name = AssertIsNullOrWhitespace(value);
        }
        public List<GroupData> GroupData { get; set; }
#nullable enable
    }
}
