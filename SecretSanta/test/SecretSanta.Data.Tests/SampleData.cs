using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        public const string Spongebob = "Spongebob";
        public const string Squarepants = "Squarepants";

        public const string Eugene = "Eugene";
        public const string Krabs = "Krabs";

        public const string GoldenSpatula = "Golden Spatula";
        public const string SpatulaDescription = "Patty flipping excellence redefined";
        public const string SpatulaUrl = "www.PattyPerfection.com";

        public const string Money = "MONEY MONEY MONEY!!!";
        public const string MoneyDescription = "A 5 letter word for happiness: MONEY.";
        public const string MoneyUrl = "www.10ReasonsToNotWasteADime.com";

        public const string JellyFishers = "JellyFishers";

        public const string MoneyGrubbers = "MoneyGrubbers";


        static public User CreateSpongebob => new User(Spongebob, Squarepants);
        static public User CreateMrKrabs => new User(Eugene, Krabs);
        static public Gift CreateSpongebobsSpatula => new Gift(GoldenSpatula, SpatulaDescription, SpatulaUrl, CreateSpongebob);
        static public Gift CreateMrKrabsMoney => new Gift(Money, MoneyDescription, MoneyUrl, CreateMrKrabs);
        static public Group CreateJellyFishers => new Group(JellyFishers);
        static public Group CreateMoneyGrubbers => new Group(MoneyGrubbers);


    }
}
