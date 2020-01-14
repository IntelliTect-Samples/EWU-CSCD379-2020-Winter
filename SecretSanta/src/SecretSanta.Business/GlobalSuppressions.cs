﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "Error caused by System.URI lookg for characters matching URL, url, etc. If variable does not represent a URI warning is safe to supress according to Microsoft Documentation", Scope = "member", Target = "~M:SecretSanta.Business.Gift.#ctor(System.Int32,System.String,System.String,System.String,SecretSanta.Business.User)")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Error caused by System.URI lookg for characters matching URL, url, etc. If variable does not represent a URI warning is safe to supress according to Microsoft Documentation", Scope = "member", Target = "~P:SecretSanta.Business.Gift.Url")]