using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
#nullable disable // Properties are initialize and checked in the set method.
        private string _CreatedBy;
        public string CreatedBy
        {
            get => _CreatedBy;
            set => _CreatedBy = AssertIsNullOrWhitespace(value);
        }

        public DateTime CreatedOn { get; set; }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get => _ModifiedBy;
            set => _ModifiedBy = AssertIsNullOrWhitespace(value);
        }
        public DateTime ModifiedOn { get; set; }
#nullable enable
    }
}
