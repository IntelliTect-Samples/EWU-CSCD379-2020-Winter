using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class User : UserInput, IEntity
    {
        public int Id { get; set; }
    }
}
