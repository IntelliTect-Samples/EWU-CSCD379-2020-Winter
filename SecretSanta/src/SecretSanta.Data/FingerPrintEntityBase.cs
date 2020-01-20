using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        public string CreatedBy { get=>_CreatedBy; set=>_CreatedBy = value ?? throw new ArgumentNullException(nameof(CreatedBy)); }
        private string _CreatedBy = String.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get=>_ModifiedBy; set=>_ModifiedBy = value ?? throw new ArgumentNullException(nameof(ModifiedBy)); }
        private string _ModifiedBy = String.Empty;
    }
}
