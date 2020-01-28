[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Error caused by System.URI looking for characters matching URL, url, etc. If variable does not represent a URI warning is safe to supress according to Microsoft Documentation")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1012:Abstract types should not have constructors", Justification = "Required by assignment", Scope = "type", Target = "~T:SecretSanta.Business.EntityService`1")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "Field is for testing purposes and does not contain any sensitive data, security is not a concern", Scope = "member", Target = "~F:SecretSanta.Data.Tests.TestBase._Mapper")]



