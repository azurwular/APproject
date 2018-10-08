namespace CBA.AP.Blog.Models
{
    public class ErrorResult
    {
        public string ErrorMessage { get; set; }

        public ErrorResult(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}