using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Common.Interfaces
{
   public interface IDapperService
    {
        Task<T> Get<T>(string sp, string cn, DynamicParameters parms, CommandType commandType);
        Task<IEnumerable<T>> GetAll<T>(string sp, string cn, DynamicParameters parms, CommandType commandType);
        Task Insert<T>(string sp, string cn, DynamicParameters parms, CommandType commandType);
        Task Update<T>(string sp, string cn, DynamicParameters parms, CommandType commandType);
    }
}
