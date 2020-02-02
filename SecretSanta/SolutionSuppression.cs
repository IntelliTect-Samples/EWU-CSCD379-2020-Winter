[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Error caused by System.URI looking for characters matching URL, url, etc. If variable does not represent a URI warning is safe to supress according to Microsoft Documentation")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Localization not relevant for this class")]


[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Interface needed for test to work", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.IController`1.Get~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{`0}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Interface needed for test to work", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.IController`1.Get(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{`0}}")]
