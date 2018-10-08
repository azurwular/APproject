using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;
using CBA.AP.Blog.Domain.Repositories;

namespace CBA.AP.Blog.Infrastructure.Repositories
{
    using System.Data;
    using Dapper;

    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext dbContext;

        public CommentRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            const string query =
                "INSERT INTO comments (created_by, created_at, content, post_id) VALUES (@dbcreatedby, @dbcreatedat, @dbcontent, @dbpostid); " +
                "SELECT LAST_INSERT_ID();";

            var parameters = new DynamicParameters();
            parameters.Add("@dbcreatedby", comment.CreatedBy, DbType.String);
            parameters.Add("@dbcreatedat", comment.CreatedAt, DbType.DateTime);
            parameters.Add("@dbcontent", comment.Content, DbType.String);
            parameters.Add("@dbpostid", comment.PostId, DbType.Int16);

            using (var connection = this.dbContext.GetConnection())
            {
                var newCommentId = await connection.QuerySingleAsync<int>(query, parameters);
                comment.Id = newCommentId;
                return comment;
            }
        }

        public async Task UpdateAsync(Comment comment)
        {
            const string query = "UPDATE comments " +
                "SET content = @dbcontent" +
                "WHERE id = @dbid";

            var parameters = new DynamicParameters();
            parameters.Add("@dbcontent", comment.Content);
            parameters.Add("@dbid", comment.Id);

            using (var connection = this.dbContext.GetConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}