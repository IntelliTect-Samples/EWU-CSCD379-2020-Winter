namespace SecretSanta.Data
{

    public class UserGroupRelation
    {

        public int   UserId  { get; set; }
        public int   GroupId { get; set; }
        public User  User    { get; set; }
        public Group Group   { get; set; }

    }

}
