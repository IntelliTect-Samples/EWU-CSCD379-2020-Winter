using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Data.Tests
{
    static public class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        public const string InigoMontoyaEmail = "inigo@montoya.me";

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";
        public const string PrincessButtercupEmail = "inigo@montoya.me";

        static public Author CreateInigoMontoya() => new Author(Inigo, Montoya, InigoMontoyaEmail);
        static public Author CreatePrincessButtercup() => new Author(Princess, Buttercup, PrincessButtercupEmail);
    }
}