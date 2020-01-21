using System;

namespace SecretSanta.Data
{
    public abstract class FingerPrintEntityBase : EntityBase
    {
#nullable disable
        public string CreatedBy { get; set; }
#nullable enable

        public DateTime CreatedOn { get; set; }

        private string? _ModifiedBy;
        public string ModifiedBy
        {
#nullable disable
            get => _ModifiedBy;
#nullable enable
            set => _ModifiedBy = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime ModifiedOn { get; set; }
    }
}
