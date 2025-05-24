using System.Data.Common;
using MySqlConnector;
using SlaveWorkColab.Api.DataAccess.Interfaces;

namespace SlaveWorkColab.Api.DataAccess;

public class DbContext : IDbContext
{
    private readonly IConfiguration _config;
    
    private MySqlConnection _connection;

    public DbContext(IConfiguration config)
    {
        _config = config;
    }

    public DbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
            return _connection;
        }
        
    }
}
