using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    class FingerPrintEntityBase : EntityBase
    {
        public FingerPrintEntityBase(
            int id, string createdBy, DateTime createdOn, string modifiedBy, DateTime modifiedOn)
            : base(id)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            ModifiedBy = modifiedBy;
            ModifiedOn = modifiedOn;
        }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
