using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        public int Id { get; set; }

        public static string AssertIsNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrEmpty(temp) => throw new ArgumentException($"{nameof(value)} cannot be just whitespace.", nameof(value)),
                _ => value
            };
    }
}
