using AutoMapper;
using Microsoft.Data.SqlClient;
using MyFirstWebAPI.Application.CountryManagement.Queries;
using MyFirstWebAPI.Infrastructure.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Services
{
    /// <summary>
    /// Service Repository Pattern 
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Reference: https://learn.microsoft.com/en-us/ef/core/querying/sql-queries
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CountryDTO>> GetCountriesByName(string name)
        {
            var parameter = new[] {             
                new SqlParameter("@name", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = name },
             };
            var sql = string.Format(@"SELECT * FROM Countries WHERE [Name] LIKE '%{0}%'", name);
            var countries = await _countryRepository.READbyStoredProcedure(sql, parameter);
            var countriesDTO = _mapper.Map<List<CountryDTO>>(countries);
            return countriesDTO;
        }

        public async Task<int> UpdateCountryActiveStatus(Guid id, bool isActive)
        {
            var parameter = new[] {
                new SqlParameter("@id", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = id.ToString() },
                new SqlParameter("@isActive", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = isActive }
             };
            var sql = string.Format(@"UPDATE Countries SET IsActive = {1} WHERE Id = '{0}'", id.ToString(), isActive ? 1 : 0);
            var result = await _countryRepository.CUDbyStoredProcedure(sql, parameter);
            return result;
        }
    }
}
