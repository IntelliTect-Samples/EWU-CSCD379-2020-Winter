using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class UserInput : IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int Id { get; set; }
    }
}
