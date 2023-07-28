using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Application.CountryManagement.Queries
{
    public class OneCountryDTO
    {
        public CountryDTO Country { get; set; }
        public List<CountryDTO> Countries { get; set; }
    }
}
