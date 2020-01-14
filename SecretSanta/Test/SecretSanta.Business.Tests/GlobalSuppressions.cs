// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test methods should contain underscores", Scope = "namespaceanddescendants", Target = "SecretSanta.Business.Tests")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Assignment requires a string per instructions", Scope = "member", Target = "~M:SecretSanta.Business.Tests.GiftTests.GiftCreate_NullData_ThrowsException(System.Int32,System.String,System.String,System.String)")]
