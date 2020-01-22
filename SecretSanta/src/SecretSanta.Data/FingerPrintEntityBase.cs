using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
#nullable disable
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
#nullable enable
    }
}
