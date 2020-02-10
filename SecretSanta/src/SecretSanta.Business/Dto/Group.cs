using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class Group : GroupInput, IEntity
    {
        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (obj is Group group)
            {
                if (Id != group.Id)
                    return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
