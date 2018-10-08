using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;
using CBA.AP.Blog.Domain.Repositories;
using CBA.AP.Blog.Services.Interfaces;

namespace CBA.AP.Blog.Services
{
    using System;

    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<Post> GetAsync(int id)
        {
            return await this.postRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAsync(int count, int page)
        {
            return await this.postRepository.GetAsync(count, page);
        }

        public async Task<Post> CreateAsync(string title, string content)
        {
            var newPost = new Post { Title = title, Content = content, Created = DateTime.UtcNow };

            return await this.postRepository.CreateAsync(newPost);
        }

        public async Task UpdateAsync(Post post)
        {
            post.LastModified = DateTime.UtcNow;

            await this.postRepository.UpdateAsync(post);
        }
    }
}