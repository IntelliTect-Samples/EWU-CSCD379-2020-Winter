using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class Gift : GiftInput, IEntity
    {
        public int Id { get; set; }
    }
}
