// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

//currently does nothing. just added per assignment requirements

// SecretSanta.Api
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "fine to suppress, as per gitter discussion", Scope = "member", Target = "~M:SecretSanta.Api.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Usage later fails for a static class", Scope = "type", Target = "~T:SecretSanta.Api.Startup")]

// SecretSanta.Business
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "specs ask for strings", Scope = "member", Target = "~M:SecretSanta.Business.Gift.#ctor(System.Int32,System.String,System.String,System.String,SecretSanta.Business.User)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "specs ask for strings", Scope = "member", Target = "~P:SecretSanta.Business.Gift.Url")]

// SecretSanta.Web
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Usage later fails for a static class", Scope = "type", Target = "~T:SecretSanta.Web.Startup")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "fine to suppress, as per gitter discussion", Scope = "member", Target = "~M:SecretSanta.Web.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]

// SecretSanta.Business.Tests
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.GiftTests.Create_Gift_Success")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.GiftTests.Create_VerifyTitleNotNullable")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.UserTests.Create_User_Success_3args")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.UserTests.Create_User_Success_4args")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.UserTests.Create_VerifyFirstNameNotNullable")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Doesn't Apply to Tests", Scope = "member", Target = "~M:SecretSanta.Business.Tests.UserTests.Create_VerifyLastNameNotNullable")]
