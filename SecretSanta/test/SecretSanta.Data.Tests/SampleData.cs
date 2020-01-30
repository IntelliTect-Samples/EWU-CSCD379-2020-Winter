using System;
using System.Security.Cryptography.X509Certificates;

namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {
        // Gifts
        public const string GiftJunk_Title = "junk";
        public const string GiftJunk_Url = "www.cheapcrap.com";
        public const string GiftJunk_Description = "completely worthless item";
        static public Gift CreateGift_Junk() => new Gift(GiftJunk_Title, GiftJunk_Url, GiftJunk_Description);
        static public Gift CreateGift_Junk(User user) => new Gift(GiftJunk_Title, GiftJunk_Url, GiftJunk_Description, user);

        public const string GiftDoorbell_Title = "Ring Doorbell";
        public const string GiftDoorbell_Url = "www.ring.com";
        public const string GiftDoorbell_Description = "The doorbell that saw too much";
        static public Gift CreateGift_Doorbell() => new Gift(GiftDoorbell_Title, GiftDoorbell_Url, GiftDoorbell_Description);
        static public Gift CreateGift_Doorbell(User user) => new Gift(GiftDoorbell_Title, GiftDoorbell_Url, GiftDoorbell_Description, user);
        
        public const string GiftArduino_Title = "Arduino";
        public const string GiftArduino_Url = "www.arduino.com";
        public const string GiftArduino_Description = "Every good geek needs an IOT device";
        static public Gift CreateGift_Arduino() => new Gift(GiftArduino_Title, GiftArduino_Url, GiftArduino_Description);
        static public Gift CreateGift_Arduino(User user) => new Gift(GiftArduino_Title, GiftArduino_Url, GiftArduino_Description, user);

        // Users
        public const string UserInigoMontoya_FName = "Inigo";
        public const String UserInigoMontoya_LName = "Montoya";
        static public User CreateUser_InigoMontoya() => new User(UserInigoMontoya_FName, UserInigoMontoya_LName);

        public const string UserPrincessButtercup_FName = "Princess";
        public const String UserPrincessButtercup_LName = "Buttercup";
        static public User CreateUser_PrincessButtercup() => new User(UserPrincessButtercup_FName, UserPrincessButtercup_LName);

        // Groups
        public const string GroupCast_Title = "Cast";
        static public Group CreateGroup_Cast() => new Group(GroupCast_Title);

        public const string GroupForest_Title = "Enchanted Forest";
        static public Group CreateGroup_Forest() => new Group(GroupForest_Title);
    }
}
