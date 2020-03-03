using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
[assembly:
    SuppressMessage("Reliability",
                    "CA2007:Consider calling ConfigureAwait on the awaited task",
                    Justification = "<Pending>")]
[assembly:
    SuppressMessage("Design",
                    "CA1054:Uri parameters should not be strings",
                    Justification =
                        "Error caused by System.URI looking for characters matching URL, url, etc. If variable does not represent a URI warning is safe to suppress according to Microsoft Documentation")]
[assembly:
    SuppressMessage("Globalization",
                    "CA1307:Specify StringComparison",
                    Justification = "Localization not relevant for this class")]
