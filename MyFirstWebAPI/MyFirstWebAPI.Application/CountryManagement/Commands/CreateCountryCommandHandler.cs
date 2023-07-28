using AutoMapper;
using MediatR;
using MyFirstWebAPI.Application.Common.ErrorHandlers;
using MyFirstWebAPI.Domain;
using MyFirstWebAPI.Infrastructure.Common.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Commands
{
    /// <summary>
    /// Mediator Pattern
    /// </summary>
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository; 
        public CreateCountryCommandHandler(IMapper mapper,
        ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<bool> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("CreateCountryCommandHandler");
                var country = _mapper.Map<CreateCountryCommand, Country>(request);

                _countryRepository.Create(country);

                return await _countryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error("CreateCountryCommandHandler " + ex.Message);
                throw new CustomErrorException("CreateCountryCommandHandler " + ex.Message);
            }
        }
    }
}
