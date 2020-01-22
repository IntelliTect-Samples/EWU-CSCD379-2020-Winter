using System;
using static SecretSanta.Data.ProjectAsserts;

namespace SecretSanta.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        public string CreatedBy
        {
            get=>_CreatedBy; 
            set=>_CreatedBy = AssertStringNotNullOrEmpty(nameof(CreatedBy), value);
        }
        private string _CreatedBy = String.Empty;

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy
        {
            get=>_ModifiedBy; 
            set=>_ModifiedBy = AssertStringNotNullOrEmpty(nameof(ModifiedBy), value);
        }
        private string _ModifiedBy = String.Empty;
    }
}
