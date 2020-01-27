﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using System.ComponentModel;

namespace SecretSanta.Data
{
    public class EntityBase
    {
        [Required]
        public int Id { get; protected set; }

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
