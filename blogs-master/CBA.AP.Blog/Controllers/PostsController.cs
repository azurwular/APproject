using System.Threading.Tasks;
using CBA.AP.Blog.Requests;
using CBA.AP.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CBA.AP.Blog.Controllers
{
    [Route("api/blog/posts")]
    public class PostsController
    {
        private readonly IPostService postService;


        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetPostsRequest request)
        {
            // comments not included
            var posts = await this.postService.GetAsync(request.Count, request.Page);

            return new JsonResult(posts);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(int postId)
        {
            // comments are included
            var post = await this.postService.GetAsync(postId);

            return new JsonResult(post);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUpdatePostRequest request)
        {
            var createdPost = await this.postService.CreateAsync(request.Title, request.Content);

            return new JsonResult(createdPost);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] CreateUpdatePostRequest request)
        {
            if (request.Id == null)
            {
                return new BadRequestResult();
            }

            var existingPost = await this.postService.GetAsync(request.Id.Value);

            if (existingPost == null)
            {
                return new BadRequestResult();
            }

            existingPost.Title = request.Title;
            existingPost.Content = request.Content;

            await this.postService.UpdateAsync(existingPost);

            return new OkResult();
        }
    }
}