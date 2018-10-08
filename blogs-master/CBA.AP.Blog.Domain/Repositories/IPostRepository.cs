using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;

namespace CBA.AP.Blog.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetAsync(int id);

        Task<IEnumerable<Post>> GetAsync(int count, int page);

        Task<Post> CreateAsync(Post post);

        Task UpdateAsync(Post post);
    }
}