using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        // Create Gifts
        public const string RingTitle = "Ring Doorbell";
        public const string RingUrl = "www.ring.com";
        public const string RingDescription = "The doorbell that saw too much";

        public const string ArduinoTitle = "Arduino";
        public const string ArduinoUrl = "www.arduino.com";
        public const string ArduinoDescription = "Every good geek needs and IOT device";

        static public Gift CreateRingGift() => new Gift(RingTitle, RingUrl, RingDescription, CreateInigoMontoya());
        static public Gift CreateArduinoGift() => new Gift(RingTitle, RingUrl, RingDescription, CreateJackRyan());

        // Create Users
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        public const string InigoMontoyaUsername = "imontoya";

        public const string Jack = "Jack";
        public const string Ryan = "Ryan";
        public const string JackRyanUsername = "jryan";

        public const string Jerett = "Jerett";
        public const string Latimer = "Latimer";
        public const string JerettLatimerUsername = "jlatimer";

        static public User CreateInigoMontoya() => new User(Inigo, Montoya);
        static public User CreateJackRyan() => new User(Jack, Ryan);
        static public User CreateJerettLatimer() => new User(Jerett, Latimer);

        // Create Groups
        public const string RedTeam = "Red Team";
        public const string BlueTeam = "Blue Team";

        static public Group CreateRedTeam => new Group(RedTeam);
        static public Group CreateBlueTeam => new Group(BlueTeam);
    }
}
