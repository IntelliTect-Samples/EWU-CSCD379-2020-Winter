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
        private ICollection<Gift> _Gifts = new List<Gift>();

        public ICollection<Gift> Gifts { get => _Gifts; set => _Gifts = value ?? throw new ArgumentNullException(nameof(value)); }

        public User? Santa { get; set; }
    }
}
