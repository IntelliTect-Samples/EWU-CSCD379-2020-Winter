using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public  class FingerPrintEntityBase : EntityBase
    {
#nullable disable
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
#nullable enable
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
