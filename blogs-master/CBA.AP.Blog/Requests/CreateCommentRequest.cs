using System.ComponentModel.DataAnnotations;

namespace CBA.AP.Blog.Requests
{
    public class CreateCommentRequest
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Content must not be empty", AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}