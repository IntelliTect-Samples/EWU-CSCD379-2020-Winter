using System;

namespace SecretSanta.Business
{
    public class BlogPost
    {


        public string Title { get; }
        public string Content { get; }
        public DateTime BlogDateTime { get; }
        public string Author { get; }

        public BlogPost(string title, string content, DateTime blogDateTime, string author)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            BlogDateTime = blogDateTime;
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }
    }
}