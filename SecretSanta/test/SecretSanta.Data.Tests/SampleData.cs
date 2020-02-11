using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {
        public const string FirstName = "Inigo";
        public const string LastName = "Montoya";
        public const string NameIdentifier = "imontoya";

        public const string GiftTitle = "Ring Doorbell";
        public const string GiftUrl = "www.ring.com";
        public const string GiftDesc = "The doorbell that saw too much";

        public const string GroupTitle = "Title";

        public static Gift CreateSampleGift() => new Gift(GiftTitle, GiftDesc, GiftUrl, CreateSampleUser());
        public static User CreateSampleUser() => new User(FirstName, LastName);
        public static Group CreateSampleGroup() => new Group(GroupTitle);
    }
}