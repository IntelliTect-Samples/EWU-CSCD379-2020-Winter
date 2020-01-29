using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";

        public const string GiftTitle1 = "Ring Doorbell";
        public const string GiftDescription1 = "The doorbell that saw too much";
        public const string GiftUrl1 = "www.ring.com";

        public const string GiftTitle2 = "Arduino";
        public const string GiftDescription2 = "Every good geek needs an IOT device";
        public const string GiftUrl2 = "www.arduino.com";

        static public User CreateInigoMontoya() => new User(Inigo, Montoya);
        static public User CreatePrincessButtercup() => new User(Princess, Buttercup);

        static public Gift CreateGift1() => new Gift(GiftTitle1, GiftDescription1, GiftUrl1, CreateInigoMontoya());
        static public Gift CreateGift2() => new Gift(GiftTitle2, GiftDescription2, GiftUrl2, CreatePrincessButtercup());

    }
}
