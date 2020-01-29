namespace SecretSanta.Data.Tests
{

    public static class SampleData
    {

        /* Users */
        public const string Jim     = "Jim";
        public const string Halpert = "Halpert";

        public const string Kevin  = "Kevin";
        public const string Malone = "Malone";

        public static User CreateJimHalpert()  => new User(Jim, Halpert);
        public static User CreateKevinMalone() => new User(Kevin, Malone);

        /* Gifts */
        public const string TeapotTitle = "Teapot";
        public const string TeapotDesc  = "It comes with bonus gifts!";
        public const string TeapotUrl   = "https://yearbook-teapot.com/";

        public const string ChiliTitle = "Chili";
        public const string ChiliDesc  = "Straight from the carpet";
        public const string ChiliUrl   = "https://chilis.com/kevin";

        public static Gift CreateTeapotGift() => new Gift(TeapotTitle, TeapotDesc, TeapotUrl, CreateJimHalpert());
        public static Gift CreateChiliGift()  => new Gift(ChiliTitle, ChiliDesc, ChiliUrl, CreateKevinMalone());

        /* Groups */
        public const string PaperCompany1 = "Dunder Mifflin Paper Company";
        public const string PaperCompany2 = "Michael Scott Paper Company";

        public static Group CreatePaperCompany1() => new Group(PaperCompany1);
        public static Group CreatePaperCompany2() => new Group(PaperCompany2);

    }

}
