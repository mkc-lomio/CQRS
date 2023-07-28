using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Domain
{
    public class TodoItem : EntityBase
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
