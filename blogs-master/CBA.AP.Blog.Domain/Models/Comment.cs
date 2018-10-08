using System;

namespace CBA.AP.Blog.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }
    }
}