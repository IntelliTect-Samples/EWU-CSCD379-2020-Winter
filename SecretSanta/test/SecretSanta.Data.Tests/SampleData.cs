namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {
        public const string firstName1 = "this is a name";
        public const string lastName1 = "this is a last name";

        public const string firstName2 = "this ones different but kind of the same";
        public const string lastName2 = "maybe these wont rhyme some day... mmmm";

        public const string firstName3 = "okay that one was pretty lame";
        public const string lastName3 = "i swear the next ones will be more tame";


        public const string url1 = "http://www.notavirus.com/";
        public const string title1 = "This Website is not a Virus";
        public const string desc1 = "Helpful website that doesn't try to harm you or your computer";

        public const string url2 = "https://www.literallyitshttpshowcoulditbemalicious.com";
        public const string title2 = "Safe and Secured Website";
        public const string desc2 = "Since we use HTTPS, it is impossible for this site to do anything you don't want";


        public const string group1 = "puorg";


        public static User User1 => new User(firstName1, lastName1);
        public static User User2 => new User(firstName2, lastName2);
        public static User User3 => new User(firstName3, lastName3);

        public static Gift Gift1 => Gift1User(User1);
        public static Gift Gift2 => Gift2User(User2);

        public static Gift Gift1User(User user) => new Gift(title1, desc1, url1, user);
        public static Gift Gift2User(User user) => new Gift(title2, desc2, url2, user);

        public static Group Group1 => new Group(group1);
    }
}