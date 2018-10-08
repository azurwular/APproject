using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;

namespace CBA.AP.Blog.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);

        Task UpdateAsync(Comment comment);
    }
}