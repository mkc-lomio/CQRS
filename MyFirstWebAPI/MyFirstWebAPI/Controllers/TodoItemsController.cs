using MyFirstWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Controllers
{
    /// <summary>
    /// Reference: https://dev.to/guivern/build-a-generic-crud-api-with-asp-net-core-adf
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TodoItemsController : GenericControllerBase<TodoItem>
    {
        public TodoItemsController(MyFirstWebAPIDbContext context)
        : base(context) { }
    }
}
