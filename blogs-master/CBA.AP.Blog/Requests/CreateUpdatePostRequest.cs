using System.ComponentModel.DataAnnotations;

namespace CBA.AP.Blog.Requests
{
    public class CreateUpdatePostRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title must not be empty", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content must not be empty", AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}