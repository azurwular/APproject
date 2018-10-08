using System.ComponentModel.DataAnnotations;

namespace CBA.AP.Blog.Requests
{
    public class GetPostsRequest
    {
        [Range(1, 10, ErrorMessage = "Count must be from 1 to 10")]
        public int Count { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page must be positive")]
        public int Page { get; set; }
    }
}