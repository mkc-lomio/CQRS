using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Domain
{
    public class Risk : EntityBase
    {
        public string PolicyNumber { get; set; }
    }
}
