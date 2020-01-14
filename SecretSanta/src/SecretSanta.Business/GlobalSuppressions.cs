// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Do not know what effects static will have on this class.", Scope = "member", Target = "~M:SecretSanta.Business.Gift.AssertIsNotNullOrWhitespace(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Do not know what effects static will have on this class.", Scope = "member", Target = "~M:SecretSanta.Business.User.AssertIsNotNullOrWhitespace(System.String)~System.String")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Do not know if we want to use System.Uri instead.", Scope = "member", Target = "~P:SecretSanta.Business.Gift.Url")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Do not know if we want to use System.Uri instead.", Scope = "member", Target = "~M:SecretSanta.Business.Gift.#ctor(System.Int32,System.String,System.String,System.String,SecretSanta.Business.User)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Do not know if we want to use System.Uri instead.", Scope = "member", Target = "~M:SecretSanta.Business.Gift.AssertIsNotNullOrWhitespace(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Do not know if we want to use System.Uri instead.", Scope = "member", Target = "~M:SecretSanta.Business.User.AssertIsNotNullOrWhitespace(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>", Scope = "member", Target = "~P:SecretSanta.Business.User.Gifts")]