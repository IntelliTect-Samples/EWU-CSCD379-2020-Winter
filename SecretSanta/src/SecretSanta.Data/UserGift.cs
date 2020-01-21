using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class UserGift
    {
#nullable disable
        public User User { get; set; }
        public int UserId { get; set; }
        public Gift Gift { get; set; }
        public int GiftId { get; set; }
#nullable enable
    }
}
