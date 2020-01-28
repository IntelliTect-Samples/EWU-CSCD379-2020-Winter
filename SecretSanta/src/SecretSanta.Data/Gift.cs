using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BlogEngine.Data
{
    public class Post : FingerPrintEntityBase
    {
        public DateTime PostedOn { get; private set; }
        public bool IsPublished { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<Comment> Comments { get; } = new List<Comment>();
        public Author Author { get; set; }
        public string? Slug { get; set; }

        public List<PostTag> PostTags { get; } = new List<PostTag>();

        public Post(string title, string content, Author author) :
            this(title, content)
        {
            Author = author;
        }

// Justification: Used by entity framework to instantiate object from the database.
#pragma warning disable IDE0051 // Remove unused private members.  Ignore because the constructor is used by entity framework.
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        private Post(
#nullable enable
#pragma warning restore IDE0051 // Remove unused private members
            string title, string content)
        {
            Title = title;
            Content = content;
            //AuthorId = authorId;
        }
    }
}
