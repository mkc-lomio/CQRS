using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyFirstWebAPI.Application.Common.ViewModels;
using MyFirstWebAPI.Application.CountryManagement.Commands;
using MyFirstWebAPI.Application.CountryManagement.Queries;
using MyFirstWebAPI.Application.CountryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryQueries _countryQueries;
        private readonly ICountryService _countryService;
        private readonly IConfiguration _configuration;
        public CountryController(ILogger<CountryController> logger,
            IMediator mediator,
            ICountryQueries countryQueries,
            ICountryService countryService,
            IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _countryQueries = countryQueries;
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _configuration = configuration;
        }

        [HttpPost("", Name = "createCountry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateCountryAsync([FromBody] CreateCountryCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                if (!commandResult)
                {
                    return StatusCode(400, "Unable to process the request");
                }

                return Ok(commandResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("countries", Name = "getCountry")]
        [ProducesResponseType(typeof(List<CountryDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetCountries()
        {
            try
            {
                var result = await _countryQueries.GetCountries();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("countrywithcountries", Name = "getCountryById")]
        [ProducesResponseType(typeof(OneCountryDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetCountry([FromQuery] Guid id)
        {
            try
            {
                var result = await _countryQueries.GetCountryById(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("paginatedcountry", Name = "getPaginatedCountries")]
        [ProducesResponseType(typeof(PaginationViewModel<CountryDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetPaginatedCountries([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string search = "")
        {
            try
            {
                var result = await _countryQueries.GetPaginatedCountries(pageSize, pageNumber, search);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("countries/name", Name = "getCountryByName")]
        [ProducesResponseType(typeof(List<CountryDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetCountryByName(string name)
        {
            try
            {
                var result = await _countryService.GetCountriesByName(name);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("countries/status", Name = "updateCountryActiveStatus")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateCountryActiveStatus([FromQuery] Guid id, [FromQuery] bool isActive)
        {
            try
            {
                var result = await _countryService.UpdateCountryActiveStatus(id, isActive);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
