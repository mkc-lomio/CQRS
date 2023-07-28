using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyFirstWebAPI.Application.Common.ErrorHandlers;
using MyFirstWebAPI.Application.Common.ViewModels;
using MyFirstWebAPI.Infrastructure.Common.Constants;
using MyFirstWebAPI.Infrastructure.Common.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Queries
{
    public class CountryQueries : ICountryQueries
    {
        private readonly IDapperService _dapperService;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        public CountryQueries(IDapperService dapperService, IConfiguration configuration)
        {
            _dapperService = dapperService;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:MyFirstWebAPIDb"); 
        }

        public async Task<List<CountryDTO>> GetCountries()
        {
            try
            {
                var query = string.Format("SELECT * FROM Countries");
                var countries = await _dapperService.GetAll<CountryDTO>(query, ConnectionString.MyFirstWebAPIDb, null, commandType: CommandType.Text);
                return countries.ToList();
            }
            catch(Exception ex)
            {
                Log.Error("Countries " + ex.Message);
                throw new CustomErrorException("Countries " + ex.Message);
            }
        }

        public async Task<OneCountryDTO> GetCountryById(Guid id)
        {
            try
            {
                var sql = @"SELECT * FROM Countries WHERE Id = @id
                            SELECT * FROM Countries";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var country = new OneCountryDTO();

                    using (var multi = await connection.QueryMultipleAsync(sql, new { id = id }))
                    {
                        country.Country = multi.Read<CountryDTO>().Single();
                        country.Countries = multi.Read<CountryDTO>().ToList();
                    }

                    connection.Close();

                    return country;
                }
            }
            catch(Exception ex)
            {
                Log.Error("GetCountryById " + ex.Message);
                throw new CustomErrorException("GetCountryById " + ex.Message);
            }
           
        }

        public async Task<PaginationViewModel<CountryDTO>> GetPaginatedCountries(int pageSize, int pageNumber, string search)
        {
            try
            {
                var sql = @"DECLARE @PageNumber AS INT
DECLARE @RowsOfPage AS INT

SET @PageNumber=@pgNumber
SET @RowsOfPage=@pgSize

SELECT * FROM
Countries c
ORDER BY
c.[Name]  
OFFSET (@PageNumber-1)*@RowsOfPage ROWS 
FETCH NEXT @RowsOfPage 
ROWS ONLY;

SELECT Count(*) FROM
( SELECT * FROM
Countries c
WHERE 
c.[Name] Like '%@search%'
) A ";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(sql, new { pgSize = pageSize, pgNumber = pageNumber, search = search }))
                    {
                        var data = multi.Read<CountryDTO>().ToList();
                        var dataCount = multi.Read<int>().Single();
                        var virtualTourPaginationViewModel = new PaginationViewModel<CountryDTO>(pageNumber, pageSize, dataCount, data);

                        connection.Close();

                        return virtualTourPaginationViewModel;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetPaginatedCountries " + ex.Message);
                throw new CustomErrorException("GetPaginatedCountries " + ex.Message);
            }
        }
    }
}
