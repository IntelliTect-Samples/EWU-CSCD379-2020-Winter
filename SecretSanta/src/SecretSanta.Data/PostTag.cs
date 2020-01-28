using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data
{
    public class PostTag
    {
        public int PostId { get; }
        public Post? Post { get; set; }
        public int TagId { get; }
        public Tag? Tag { get; }

        public PostTag(Post post, Tag tag)
        {
            Post = post ?? throw new ArgumentNullException(nameof(post));
            PostId = post.Id??default;
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
            TagId = tag.Id??default;
        }
// Justification: Private member for entity framework instantiation only.
#pragma warning disable IDE0051 // Remove unused private members
#nullable disable // CS8618: Non-nullable field is uninitialized. Consider declaring as nullable.
        private PostTag(
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning restore IDE0051 // Remove unused private members
            int postId, int tagId)
        {
            PostId = postId;
            TagId = tagId;
        }
    }
}
