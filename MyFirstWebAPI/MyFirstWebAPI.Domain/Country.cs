using MyFirstWebAPI.Domain.Base;
using System;

namespace MyFirstWebAPI.Domain
{
    public class Country : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string FlagUri { get; set; }
    }
}
