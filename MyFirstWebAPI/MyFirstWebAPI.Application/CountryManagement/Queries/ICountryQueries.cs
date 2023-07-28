using MyFirstWebAPI.Application.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Queries
{
    public interface ICountryQueries
    {
        Task<List<CountryDTO>> GetCountries();
        Task<OneCountryDTO> GetCountryById(Guid id);
        Task<PaginationViewModel<CountryDTO>> GetPaginatedCountries(int pageSize, int pageNumber, string search);
    }
}
