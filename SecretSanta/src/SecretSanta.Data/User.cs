using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class User : FingerPrintEntityBase
    {
        public string FirstName { get => _FirstName; set => _FirstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        private string _FirstName = string.Empty;
        public string LastName { get => _LastName; set => _LastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        private string _LastName = string.Empty;
#nullable disable // Properties are initialize and checked in the set method.
        private List<Gift> _Gifts = new List<Gift>();
        public List<Gift> Gifts
        {
            get => _Gifts;
            set => _Gifts = value ?? throw new ArgumentNullException(nameof(value));
        }
        private List<GroupData> _GroupData = new List<GroupData>();
        public List<GroupData> GroupData
        {
            get => _GroupData;
            set => _GroupData = value ?? throw new ArgumentNullException(nameof(value));
        }
#nullable enable
        public User? Santa { get; set; }
    }
}
