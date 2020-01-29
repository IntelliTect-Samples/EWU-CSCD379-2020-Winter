namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";

        private const string Title = "Ring Doorbell";
        private const string Description = "www.ring.com";
        private const string Url = "The doorbell that saw too much";

        private const string TitleArduino = "Arduino";
        private const string DescriptionArduino = "www.arduino.com";
        private const string UrlArduino = "Every good geek needs an IOT device";

        public static User CreateInigoMontoya() => new User(Inigo, Montoya);
        public static User CreatePrincessButtercup() => new User(Princess, Buttercup);

        public static Gift CreateGift() =>
            (new Gift(Title, Description, Url, CreateInigoMontoya()));

        public static Gift CreateGift(User user) =>
            (new Gift(Title, Description, Url, user));
        public static Gift CreateGiftArduino() => new Gift(TitleArduino, DescriptionArduino, UrlArduino,
            SampleData.CreatePrincessButtercup());

    }
}