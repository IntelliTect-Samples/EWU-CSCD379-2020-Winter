using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    class Gift
    {
        private readonly int ID;
        private string Title { get; set; }
        private string Description { get; set; }
        private string Url { get; set; }
        private User User { get; set; }
    }
}
