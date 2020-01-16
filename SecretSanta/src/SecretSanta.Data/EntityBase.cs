using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    class EntityBase
    {
        public int Id { get; }

        public EntityBase(int id)
        {
            Id = id;
        }
    }
}
