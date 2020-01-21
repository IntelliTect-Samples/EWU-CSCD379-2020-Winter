

namespace SecretSanta.Data
{
    public class UserGroup
    {
        
#nullable disable
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
#nullable enable
    }
}
