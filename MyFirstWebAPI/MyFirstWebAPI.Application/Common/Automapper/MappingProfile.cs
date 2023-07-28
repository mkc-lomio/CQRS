using AutoMapper;
using MyFirstWebAPI.Application.CountryManagement.Commands;
using MyFirstWebAPI.Application.CountryManagement.Queries;
using MyFirstWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.Common.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Command to Entity
            createMapForCountry();
            #endregion
        }

        private void createMapForCountry()
        {
            CreateMap<CreateCountryCommand, Country>().ForMember(dest => dest.FlagUri, opt => opt.MapFrom(mf => mf.Name));
            CreateMap<CountryDTO, Country>().ReverseMap();        }
    }
}
