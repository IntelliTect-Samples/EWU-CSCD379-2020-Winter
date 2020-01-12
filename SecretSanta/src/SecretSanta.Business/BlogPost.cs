using System;

namespace SecretSanta.Business
{
    public class BlogPost
    {

        public DateTime Time { get; }

        public string Author { get; set; }

        public string Title { get; }

        public string Content
        {
            get => _Content;
            set => _Content = value ?? throw new ArgumentNullException(nameof(value));
        } private string _Content;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public BlogPost(string title, string content, DateTime time, string author)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content;
            Time = time;
            Author = author;
        }

    }
}