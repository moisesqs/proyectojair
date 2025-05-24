using System.Data.Common;

namespace SlaveWorkColab.Api.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
}