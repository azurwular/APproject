using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;

namespace CBA.AP.Blog.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(int postId, string createdBy, string content);

        Task UpdateAsync(Comment comment);
    }
}