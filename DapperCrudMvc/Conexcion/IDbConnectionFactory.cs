using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace DapperCrudMvc.Conexcion
{
    //Interfaz
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }

    // Implementa la interfaz IDbConnectionFactory
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQL");
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
