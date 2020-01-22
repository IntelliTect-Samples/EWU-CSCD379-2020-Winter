using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    internal class ProjectAsserts
    {
        internal static string AssertStringNotNullOrEmpty(string nameofparam, string value)
        {
            if (string.IsNullOrWhiteSpace(value ?? throw new ArgumentNullException(nameofparam)))
                throw new ArgumentException("Parameter may not be assigned an Empty or whitespace string",
                    nameofparam);
            return value;
        }
    }
}
