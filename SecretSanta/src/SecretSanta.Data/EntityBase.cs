using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        [Required]
        public int Id { get; set; }

        static protected string AssertIsNotNullOrWhitespace(string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be an empty string.", nameof(value)),
                string temp when string.IsNullOrWhiteSpace(temp) =>
                    throw new ArgumentException($"{nameof(value)} cannot be only whitespace.", nameof(value)),
                _ => value
            };
    }
}
