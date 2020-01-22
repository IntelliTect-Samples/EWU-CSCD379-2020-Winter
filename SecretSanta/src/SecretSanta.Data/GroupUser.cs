﻿
namespace SecretSanta.Data
{
    public class GroupUser
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        #nullable disable
        public User User { get; set; }  
        public Group Group { get; set; }
        #nullable enable
    }
}
