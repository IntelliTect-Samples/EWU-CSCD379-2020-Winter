using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        private string _CreatedBy = string.Empty;
        private string _ModifiedBy = string.Empty;

        public string CreatedBy { get => _CreatedBy; private set => _CreatedBy = value ?? throw new ArgumentNullException(nameof(value)); }

        public DateTime CreatedOn { get; private set; }

        public string ModifiedBy { get => _ModifiedBy; private set => _ModifiedBy = value ?? throw new ArgumentNullException(nameof(value)); }

        public DateTime ModifiedOn { get; private set; }
    }
}
