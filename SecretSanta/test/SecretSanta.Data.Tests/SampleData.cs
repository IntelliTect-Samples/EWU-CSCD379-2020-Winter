namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        public const string Billy = "Billy";
        public const string Bob = "Bob";

        public const string Fred = "Fred";
        public const string Flintstone = "Flintstone";

        public const string CoolGroupName = "The cool group";
        public const string CrazyGroupName = "The crazy group";


        public const string GiftTitle = "Free money";
        public const string GiftDescription = "The coolest gift";
        public const string GiftUrl = "www.url.com";

        public const string CrazyGiftTitle = "Crazy Gift";
        public const string CrazyGiftDescription = "The craziest gift";
        public const string CrazyUrl = "www.crazy.com";


        public static User CreateBillyBob() => new User(Billy, Bob);
        public static User CreateFredFlintstone() => new User(Fred, Flintstone);
        public static Gift CreateCoolGift() => new Gift(GiftTitle, GiftDescription, GiftUrl, CreateFredFlintstone());
        public static Gift CreateCrazyGift() => new Gift(CrazyGiftTitle, CrazyGiftDescription, CrazyUrl, CreateBillyBob());
        public static Group CreateCoolGroup() => new Group(CoolGroupName);
        public static Group CreateCrazyGroup() => new Group(CrazyGroupName);
    }
}
