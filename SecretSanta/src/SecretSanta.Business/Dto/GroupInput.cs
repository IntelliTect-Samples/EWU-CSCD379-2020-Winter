using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class GroupInput : IEntity
    {
        public string? Title { get; set; }

        public int Id { get; set; }
    }
}
