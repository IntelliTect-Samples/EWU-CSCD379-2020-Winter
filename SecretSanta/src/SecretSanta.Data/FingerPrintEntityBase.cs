using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        private string _CreatedBy = string.Empty;
        private string _ModifiedBy = string.Empty;

        public string CreatedBy { get => _CreatedBy; set => _CreatedBy = value ?? throw new ArgumentNullException(nameof(value)); }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get => _ModifiedBy; set => _ModifiedBy = value ?? throw new ArgumentNullException(nameof(value)); }

        public DateTime ModifiedOn { get; set; }
    }
}
