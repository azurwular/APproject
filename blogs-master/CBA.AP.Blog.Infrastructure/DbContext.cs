using System.Data;
using MySql.Data.MySqlClient;

namespace CBA.AP.Blog.Infrastructure
{
    /// <summary>
    /// Factory class for <see cref="IDbConnection"/>
    /// </summary>
    public class DbContext
    {
        private readonly string connectionString;

        public DbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Factory method that generates <see cref="IDbConnection">db connections</see>
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return new MySqlConnection(this.connectionString);
        }
    }
}