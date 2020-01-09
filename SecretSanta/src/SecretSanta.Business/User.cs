using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    class User
    {
        private readonly int ID;
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private ICollection<Gift> Gifts { get; set; }
    }
}
