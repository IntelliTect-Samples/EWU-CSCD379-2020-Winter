namespace SecretSanta.Data
{
    public class UserGroup
    {
#nullable disable
        public User User { get; set; }
        public int UserId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
#nullable enable
    }
}
