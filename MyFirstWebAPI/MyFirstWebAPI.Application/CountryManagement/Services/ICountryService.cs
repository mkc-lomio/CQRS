using MyFirstWebAPI.Application.CountryManagement.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetCountriesByName(string name);
        Task<int> UpdateCountryActiveStatus(Guid id, bool isActive);
    }
}
