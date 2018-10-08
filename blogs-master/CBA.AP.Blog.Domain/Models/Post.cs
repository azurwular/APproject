using System;
using System.Collections.Generic;

namespace CBA.AP.Blog.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public List<Comment> Comments { get; set; }
    }
}