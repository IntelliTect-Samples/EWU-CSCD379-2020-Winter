using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data
{
    public class Tag : FingerPrintEntityBase
    {
        public string Name { get; set; }
        public List<PostTag> PostTags { get; } = new List<PostTag>();

        public Tag(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
