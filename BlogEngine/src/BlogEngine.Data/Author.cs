﻿using System;
using System.Collections.Generic;

namespace BlogEngine.Data
{
    public class Author : FingerPrintEntityBase
    {
        private string _FirstName = null!;
        public string FirstName
        {
            get { return _FirstName; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _FirstName = value;
            }
        }

        private string _LastName = null!;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _LastName = value;
            }
        }
        private string _Email = null!;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _Email = value;
            }
        }

        public List<Post> Posts { get; } = new List<Post>();

        public Author(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}