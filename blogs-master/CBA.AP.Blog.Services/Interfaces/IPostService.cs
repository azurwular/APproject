using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;

namespace CBA.AP.Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> GetAsync(int id);

        Task<IEnumerable<Post>> GetAsync(int count, int page);

        Task<Post> CreateAsync(string title, string content);

        Task UpdateAsync(Post post);
    }
}