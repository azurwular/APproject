using System.ComponentModel.DataAnnotations;

namespace CBA.AP.Blog.Requests
{
    public class UpdateCommentRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content must not be empty", AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}