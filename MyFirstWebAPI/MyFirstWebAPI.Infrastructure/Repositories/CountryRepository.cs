using MyFirstWebAPI.Domain;
using MyFirstWebAPI.Infrastructure.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Repositories
{
   public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(MyFirstWebAPIDbContext context) : base(context)
        {

        }
    }
}
