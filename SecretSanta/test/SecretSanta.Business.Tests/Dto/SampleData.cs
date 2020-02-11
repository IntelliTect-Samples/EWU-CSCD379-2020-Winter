using SecretSanta.Business.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Tests.Dto
{
    public static class SampleData
    {
        public const string _MrKrabsFirstName = "Eugene";
        public const string _MrKrabsLastName = "Krabs";

        public const string _SpongebobFirstName = "Spongebob";
        public const string _SpongebobLastName = "Squarepants";

        public const string _MoneyTitle = "Money!!!";
        public const string _MoneyDescription = "A five letter word for hapiness.";
        public const string _MoneyUrl = "www.10ReasonsToSaveADime.com";

        public const string _LeSpatulaTitle = "Le Spatula 3000";
        public const string _LeSpatulaDescription = "Le Spatula 2000, At your Service.";
        public const string _LeSpatulaUrl = "https://spongebob.fandom.com/wiki/Le_Spatula";

        public const string _JellySpottersTitle = "JellySpotters";

        public const string _MoneyGrubbersTitle = "MoneyGrubbers";


        public static UserInput CreateSpongebob()
        {
            return new UserInput()
            {
                FirstName = _SpongebobFirstName,
                LastName = _SpongebobLastName
            };
        }

        public static UserInput CreateMrKrabs()
        {
            return new UserInput()
            {
                FirstName = _MrKrabsFirstName,
                LastName = _MrKrabsLastName
            };
        }

        public static GiftInput CreateMoney()
        {
            return new GiftInput()
            {
                Title = _MoneyTitle,
                Description = _MoneyDescription,
                Url = _MoneyUrl
            };
        }

        public static GiftInput CreateLeSpatula()
        {
            return new GiftInput()
            {
                Title = _LeSpatulaTitle,
                Description = _LeSpatulaDescription,
                Url = _LeSpatulaUrl,
            };
        }

        public static GroupInput CreateJellySpotters()
        {
            return new GroupInput()
            {
                Title = _JellySpottersTitle
            };
        }

        public static GroupInput CreateMoneyGrubbers()
        {
            return new GroupInput()
            {
                Title = _MoneyGrubbersTitle
            };
        }

        
    }
}
