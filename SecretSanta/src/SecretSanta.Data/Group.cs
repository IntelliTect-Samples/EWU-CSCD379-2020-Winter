using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    class Group : FingerPrintEntityBase
    {
        public Group(
            int id, string createdBy, DateTime createdOn, string modifiedBy, DateTime modifiedOn,
            string name)
            : base(id, createdBy, createdOn, modifiedBy, modifiedOn)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
