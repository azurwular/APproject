using System.Threading.Tasks;
using CBA.AP.Blog.Requests;
using CBA.AP.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CBA.AP.Blog.Controllers
{
    [Route("api/blog/comments")]
    public class CommentsController
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService, IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            var post = await this.postService.GetAsync(request.PostId);

            if (post == null)
            {
                return new BadRequestResult();
            }

            var comment = this.commentService.CreateAsync(request.PostId, "user", request.Content);

            return new JsonResult(comment);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequest request)
        {
            var existingComment = await this.postService.GetAsync(request.Id);

            if (existingComment == null)
            {
                return new BadRequestResult();
            }

            existingComment.Content = request.Content;

            await this.postService.UpdateAsync(existingComment);

            return new OkResult();
        }
    }
}