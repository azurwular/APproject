using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.AP.Blog.Domain.Models;
using CBA.AP.Blog.Domain.Repositories;
using Dapper;

namespace CBA.AP.Blog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext dbContext;

        public PostRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Post> GetAsync(int id)
        {
            const string query = "SELECT p.id, p.title, p.content, p.created, p.last_modified, c.id, c.created_by, c.created_at " +
                                 "FROM posts p " +
                                 "LEFT JOIN comments c ON p.id = c.post_id " +
                                 "WHERE p.id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (var connection = this.dbContext.GetConnection())
            {
                Post result = null;

                await connection.QueryAsync<Post, Comment, Post>(
                    query,
                    (p, c) =>
                    {
                        if (result == null)
                        {
                            result = new Post
                            {
                                Id = p.Id,
                                Created = p.Created,
                                Title = p.Title,
                                Content = p.Content,
                                LastModified = p.LastModified,
                                Comments = new List<Comment>()
                            };
                        }

                        if (c != null)
                        {
                            result.Comments.Add(c);
                        }

                        return result;
                    },
                    parameters);

                return result;
            }
        }

        public async Task<IEnumerable<Post>> GetAsync(int count, int page)
        {
            const string query = "SELECT id, title, content, created, lastModified " +
                                 "FROM posts " +
                                 "ORDER BY created DESC " +
                                 "LIMIT @Count OFFSET @Offset";

            var offset = (page - 1) * count;
            var parameters = new DynamicParameters();
            parameters.Add("@Count", count);
            parameters.Add("@Offset", offset);

            using (var connection = this.dbContext.GetConnection())
            {
                return await connection.QueryAsync<Post>(query, parameters);
            }
        }

        public async Task<Post> CreateAsync(Post post)
        {
            const string query = "INSERT INTO posts (title, content, created) VALUES (@dbtitle, @dbcontent, @dbcreated); " +
                                 "SELECT LAST_INSERT_ID();";

            var parameters = new DynamicParameters();
            parameters.Add("@dbtitle", post.Title);
            parameters.Add("@dbcontent", post.Content);
            parameters.Add("@dbcreated", post.Created);

            using (var connection = this.dbContext.GetConnection())
            {
                var newPostId = await connection.QuerySingleAsync<int>(query, parameters);
                post.Id = newPostId;
                return post;
            }
        }

        public async Task UpdateAsync(Post post)
        {
            const string query = "UPDATE posts " +
                                 "SET title = @dbtitle, content = @dbcontent, last_modified = @lastModified" +
                                 "WHERE id = @dbid";

            var parameters = new DynamicParameters();
            parameters.Add("@dbtitle", post.Title);
            parameters.Add("@dbcontent", post.Content);
            parameters.Add("@dblastModified", post.LastModified);
            parameters.Add("@dbid", post.Id);

            using (var connection = this.dbContext.GetConnection())
            {
                 await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}