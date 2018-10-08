using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;
using CBA.AP.Blog.Domain.Repositories;
using CBA.AP.Blog.Services.Interfaces;

namespace CBA.AP.Blog.Services
{
    using System;

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Comment> CreateAsync(int postId, string createdBy, string content)
        {
            var newComment = new Comment
            {
                CreatedBy = createdBy,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                PostId = postId
            };

            return await this.commentRepository.CreateAsync(newComment);
        }

        public async Task UpdateAsync(Comment comment)
        {
            await this.commentRepository.UpdateAsync(comment);
        }
    }
}