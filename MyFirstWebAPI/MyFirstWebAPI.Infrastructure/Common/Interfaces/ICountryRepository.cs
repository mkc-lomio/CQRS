using MyFirstWebAPI.Domain;
using MyFirstWebAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Common.Interfaces
{
   public interface ICountryRepository : IRepository<Country>
    {
    }
}
