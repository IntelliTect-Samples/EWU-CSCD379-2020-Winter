﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {

        // User 1
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        static public User CreateInigoMontoya() => new User(Inigo, Montoya);

        // User 2
        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";
        static public User CreatePrincessButtercup() => new User(Princess, Buttercup);

        // Gift 1
        public const string Title1 = "GiftTitle1";
        public const string Desc1 = "GiftDescription1";
        public const string Url1 = "GiftUrl1";
        static public Gift CreateGift1() => new Gift(Title1, Url1, Desc1, CreateInigoMontoya());

        // Gift 2
        public const string Title2 = "GiftTitle2";
        public const string Desc2 = "GiftDescription2";
        public const string Url2 = "GiftUrl2";
        static public Gift CreateGift2() => new Gift(Title2, Url2, Desc2, CreatePrincessButtercup());

        // Group 1
        public const String GroupTitle1 = "GroupTitle1";
        static public Group CreateGroup1() => new Group(GroupTitle1);

        // Group2
        public const String GroupTitle2 = "GroupTitle2";
        static public Group CreateGroup2() => new Group(GroupTitle2);
    }
}
